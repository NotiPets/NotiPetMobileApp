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

        protected    IObservable<Response<TData>> RemoteRequestObservableAsync<TData>(IObservable<HttpResponseMessage> task,bool useRequestModel=true)
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
                                if (useRequestModel)
                                {
                                    var resultRequestOk = await Task.Run(() =>  JsonConvert.DeserializeObject<Request<TData>>(jsonResult));
                                    return Response<TData>.Ok(resultRequestOk.Data);
                                }
                                var resultOk = await Task.Run(() =>  JsonConvert.DeserializeObject<TData>(jsonResult));
                                return Response<TData>.Ok(resultOk);

                            }
                            var result = await Task.Run(() => JsonConvert.DeserializeObject<Error>(jsonResult));
                            var message = result?.Message ?? responseMessage.ReasonPhrase;
                            throw new Exception($"{message}");
                            return Response<TData>.Error(result);
                        });
                    })
                    .SelectMany(e=>e);
        }
    }
}