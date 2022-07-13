using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;

namespace NotiPet.Data.Services
{
    public class PetServiceApi:BaseApiService,IPetServiceApi
    {
        private readonly IApiClient<IPetApi> _apiClient;

        public PetServiceApi(IApiClient<IPetApi> apiClient)
        {
            _apiClient = apiClient;
        }
        public IObservable<IEnumerable<PetDto>> GetPets(string userId)
        {
           return RemoteRequestObservableAsync<IEnumerable<PetDto>>(_apiClient.Client.GetPets(userId))
                .Select(e=>e.Result);
        }

        public IObservable<PetDto> SavePets(PetDto petDto)
        {
            return RemoteRequestObservableAsync<PetDto>(_apiClient.Client.PostPets(petDto))
                .Select(e=>e.Result);
        }

        public IObservable<object> RemovePets(string id)
        {
            return RemoteRequestObservableAsync<object>(_apiClient.Client.RemovePets(id),useRequestModel:false)
                .Select(e=>e.Result);
        }

        public IObservable<PetDto> EditPet(PetDto map)
        {
            return RemoteRequestObservableAsync<Pet>(_apiClient.Client.EditPet(map.Id,map),false)
                .Select(e=>new PetDto());
        }

        public IObservable<IEnumerable<DigitalVaccineDto>> GetVaccinesByPet(string petId)
        {
            return RemoteRequestObservableAsync<IEnumerable<DigitalVaccineDto>>(_apiClient.Client.GetVaccinesByPet(petId),true)
                .Select(e=>e.Result);
        }

        public IObservable<VaccinatePdfDto> GetVaccinePdf(string vaccinateId)
        {
            return RemoteRequestObservableAsync<VaccinatePdfDto>(_apiClient.Client.GetVaccinePdf(vaccinateId),true)
                .Select(e=>e.Result);
        }
    }
}