using System;
using System.Net.Http;
using NotiPet.Data.Dtos;
using Refit;

namespace NotiPet.Data
{
    public interface IPetApi
    {
        [Get("/Pets/ByUserId/{userId}")]
        IObservable<HttpResponseMessage> GetPets(string userId);

        [Post("/Pets")]
        IObservable<HttpResponseMessage> PostPets([Body] PetDto petDto);
    }
}