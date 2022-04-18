using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using NotiPet.Data.Dtos;

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
    }
}