using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using NotiPet.Data.Dtos;

namespace NotiPet.Data.Services
{
    public class BusinessServiceApi:BaseApiService,IBusinessServiceApi
    {
        private readonly IApiClient<IBusinessApi> _apiClient;

        public BusinessServiceApi(IApiClient<IBusinessApi> apiClient)
        {
            _apiClient = apiClient;
        }
        public IObservable<IEnumerable<BusinessDto>> GetBusiness()
        {
            return RemoteRequestObservableAsync<IEnumerable<BusinessDto>>(_apiClient.Client.GetBusinesses())
                    .Select(e=>e.Result);
        }

        public IObservable<BusinessDto> GetBusiness(int id)
        {
            return RemoteRequestObservableAsync<BusinessDto>(_apiClient.Client.GetBusinesses(id))
                .Select(e=>e.Result);
        }
    }
}