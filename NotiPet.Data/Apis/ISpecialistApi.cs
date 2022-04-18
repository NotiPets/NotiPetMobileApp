using System;
using System.Net.Http;
using DynamicData;
using NotiPet.Domain.Models;
using Refit;

namespace NotiPet.Data
{
    public interface ISpecialistApi
    {
        [Get("/Specialists")]
        IObservable<HttpResponseMessage> GetSpecialists();
        [Get("/Specialities")]
        IObservable<HttpResponseMessage> GetSpecialities();
    }
}