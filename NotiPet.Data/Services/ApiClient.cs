using System;
using System.Net.Http;
using Refit;

namespace NotiPet.Data.Services
{
    public class ApiClient<T> : IApiClient<T>
    {
        Func<T> createClient;
        Lazy<T> lazyClient;

        public ApiClient(IApiProvider apiProvider)
        {
            ChangeBaseAddress(apiProvider.ApiUrl);
        }


        public T Client
        {
            get
            {
                if (lazyClient == null)
                {
                    lazyClient = new Lazy<T>(() => createClient());
                }
                return lazyClient.Value;
            }
        }


        public void ChangeBaseAddress(string baseAddress)
        {
            lazyClient = null;
            createClient = () =>
            {
                var client = new HttpClient()
                {
                    BaseAddress = new Uri(baseAddress)
                };

                return RestService.For<T>(client);
            };
            
        }
    }

    public interface IApiClient<T>
    {
        public T Client { get; }
    }
}