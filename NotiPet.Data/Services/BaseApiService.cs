using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NotiPet.Data.Dtos;
using Polly;
using Refit;

namespace NotiPet.Data.Services
{
    public class BaseApiService
    {
        protected Dictionary<int, CancellationTokenSource> runningTasks = new Dictionary<int, CancellationTokenSource>();
        protected  async  Task<Response<TData>> RemoteRequestAsync<TData>(Task<HttpResponseMessage> task)
        {
            try
            {
                HttpResponseMessage responseMessage = await Policy
                    .Handle<WebException>()
                    .Or<ApiException>()
                    .Or<TaskCanceledException>()
                    .WaitAndRetryAsync
                    (
                        retryCount: 3,
                        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () =>
                    {
                        var cts = new CancellationTokenSource();
                        runningTasks.Add(task.Id, cts);
                        var result = await task;
                        runningTasks.Remove(task.Id);

                        return result;
                    });


                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var result =  await Task.Run(() => JsonConvert.DeserializeObject<Request<TData>>(jsonResult));
                    return  Response<TData>.Ok(result.Data);

                }
                else
                {
                    var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var result = await Task.Run(() => JsonConvert.DeserializeObject<Error>(jsonResult));
                    return Response<TData>.Error(result);
                }
            }
            catch (Exception e)
            {

                throw e;
            }


        }
        protected    IObservable<Response<TData>> RemoteRequestObservableAsync<TData>(IObservable<HttpResponseMessage> task)
        {

                return task.ObserveOn(TaskPoolScheduler.Default)
                    .Timeout(TimeSpan.FromSeconds(60))
                    .Retry(3)
                    .Select( responseMessage =>
                    {
                        return  Observable.FromAsync(async token =>
                        {
                            var jsonResult = await responseMessage.Content.ReadAsStringAsync();
                            if (responseMessage.IsSuccessStatusCode)
                            {
                                var resultOk = await Task.Run(() => JsonConvert.DeserializeObject<Request<TData>>(jsonResult));
                                return Response<TData>.Ok(resultOk.Data);
                            }
                            var result = await Task.Run(() => JsonConvert.DeserializeObject<Error>(jsonResult));
                            return Response<TData>.Error(result);
                        });
                    })
                    .SelectMany(e=>e);
        }
         protected IObservable<Response<TData>> RemoteRequestObservable<TData>(Task<HttpResponseMessage> task)
         {
              return  Observable.FromAsync(token => RemoteRequestAsync<TData>(task));
         }
    }
}