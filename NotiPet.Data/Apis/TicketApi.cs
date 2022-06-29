using System;
using System.Net.Http;
using NotiPet.Data.Dtos;
using Refit;

namespace NotiPet.Data
{
     public interface ITicketApi
    {
        [Post("/Tickets")]
        IObservable<HttpResponseMessage> CreateTicket([Body]TicketDto user);
    }
}