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
        {/*
                    var serviceFixture = new AuthenticationServiceFixture()
                .MapperWith(Mapper)
                .UserServiceApiServiceWith(new UserServiceApi(new ApiClient<IUserApi>("https://noti-pet-test.herokuapp.com/api/")));
            IAuthenticationService service =(AuthenticationService) serviceFixture;
            var fixtureViewModel = new LoginViewModelFixture()
                .AuthenticationWith(service);
            IAuthenticationRequestViewModel viewModel = (LoginViewModel)fixtureViewModel;
            viewModel.Email = "waldoomaet1";
            viewModel.Password = "1234";
            service.Authentication(viewModel).Subscribe();*/
            SourceList<string> list = new SourceList<string>();
            /*var test = new UserServiceApi(new ApiClient<IUserApi>("https://noti-pet-test.herokuapp.com/api"));
           var test2 = test.LogIn(new RequestAuthenticationDto()
            {
                Username = "waldoomaet1",
                Password = "1234"
            });
           test2.Subscribe();
           Console.WriteLine( await test2);*/

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