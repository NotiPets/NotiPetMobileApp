using System;
using System.Net.Http;
using Refit;

namespace NotiPet.Data
{
    public interface IBusinessApi
    {
        [Get("/Businesses")]
        IObservable<HttpResponseMessage> GetBusinesses();
        [Get("/Businesses/{id}")]
        IObservable<HttpResponseMessage> GetBusinesses(int id);
    }
}