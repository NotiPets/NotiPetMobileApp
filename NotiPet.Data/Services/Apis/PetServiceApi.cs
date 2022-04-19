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

        public IObservable<Pet> EditPet(PetDto map)
        {
            return RemoteRequestObservableAsync<Pet>(_apiClient.Client.EditPet(map.Id,map))
                .Select(e=>e.Result);
        }
    }
}