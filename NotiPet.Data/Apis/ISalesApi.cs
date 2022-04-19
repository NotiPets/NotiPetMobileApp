using System;
using System.Net.Http;
using NotiPet.Data.Dtos;
using Refit;

namespace NotiPet.Data
{
    public interface ISalesApi
    {
        [Get("/Sales/ByUser/{userId}")]
        public IObservable<HttpResponseMessage> GetSalesOrder(string userId);
        [Post("/Sales")]
        public IObservable<HttpResponseMessage> PostSalesOrder([Body] RequestOrderDto requestOrderDto);

        [Get("/Appointments/ByUser/{userId}")]
        public IObservable<HttpResponseMessage> GetAppointmentByUser(string userId);
    }
}