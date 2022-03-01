using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;
using Refit;

namespace NotiPet.Data.Services
{
    public class BaseApiService
    {
        protected Dictionary<int, CancellationTokenSource> runningTasks = new Dictionary<int, CancellationTokenSource>();
        protected async Task<Response<TData>> RemoteRequestAsync<TData>(Task<HttpResponseMessage> task)
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
                var result = await Task.Run(() => JsonConvert.DeserializeObject<TData>(jsonResult));
                return Response<TData>.Ok(result);
            }
            else
            {
                var jsonResult = responseMessage;
                //var result = await Task.Run(() => JsonConvert.DeserializeObject<HttpResponseMessage>(jsonResult));
                return Response<TData>.Error(jsonResult);
            }

        }
    }
}