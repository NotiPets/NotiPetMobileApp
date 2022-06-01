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
using Microsoft.AspNetCore.SignalR.Client;
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
            var connections  = new HubConnectionBuilder().WithUrl("https://notipet-api.herokuapp.com/api/Appointments/Inform").Build();
            await connections.StartAsync();
            connections.On("InformClient", (string message) =>
            {
                Console.WriteLine(message);
            });
            Console.WriteLine("trs");
            Console.ReadKey();
            Console.WriteLine("dasdasd");
        }

    }

}