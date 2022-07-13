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
        [Delete("/Pets/{id}")]
        IObservable<HttpResponseMessage> RemovePets(string id);
        [Put ("/Pets/{id}")]
        IObservable<HttpResponseMessage> EditPet(string id, [Body]PetDto map);
        [Get ("/DigitalVaccine/ByPetId/{petId}")]
        IObservable<HttpResponseMessage> GetVaccinesByPet(string petId);
        [Get("/DigitalVaccine/PdfByteArray2/{id}")]
        IObservable<HttpResponseMessage> GetVaccinePdf(string id);
    }
}