using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using AutoMapper;
using DynamicData;
using ImTools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NotiPet.Data;
using NotiPet.Data.Dtos;
using NotiPet.Data.Mappers;
using NotiPet.Data.Services;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var veterinary =
                new VeterinaryService(
                    new BusinessServiceApi(
                        new ApiClient<IBusinessApi>(new ApiProvider("https://notipet-wr-dev.herokuapp.com/api"))),
                    new Mapper(AutoMapperConfig.GetConfig()));
          var a=  veterinary.GetVeterinary();
          a.Subscribe();
           foreach (var item in a)
           {
               Console.WriteLine(item);
           }
        }

        private static HttpClient client = new HttpClient();
        static  IObservable<string> DoWork()
        {

            return Observable
                .FromAsync(() => client.GetAsync("https://randomuser.me/api/?results=10&page=1"))
                .SubscribeOn(TaskPoolScheduler.Default)
                .Retry(5)
                .Timeout(TimeSpan.FromSeconds(80))
                .Do(x => Console.WriteLine($"x.IsSuccessStatusCode{x.IsSuccessStatusCode}"))
                .SelectMany(
                    async x =>
                    {
                        JArray parsedTodos = null;

                        var readAsString = await x.Content.ReadAsStringAsync().ConfigureAwait(false);
                        return readAsString;

                    })
                .Select(x => x)
                ;

        }

    }

}