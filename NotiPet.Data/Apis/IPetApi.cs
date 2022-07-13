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
        [Get ("/DigitalVaccine/ByPetId/9819dbd2-adb7-4700-95d3-ea4d53aea829")]
        IObservable<HttpResponseMessage> GetVaccinesByPet(string petId);
        [Get("/DigitalVaccine/PdfByteArray2/1094f044-24ee-4d97-96fc-cf697765002a")]
        IObservable<HttpResponseMessage> GetVaccinePdf(object petId);
    }
}