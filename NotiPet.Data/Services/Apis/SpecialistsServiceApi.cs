using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using NotiPet.Data.Dtos;

namespace NotiPet.Data.Services
{
    public class SpecialistsServiceApi:BaseApiService,ISpecialistServiceApi
    {
        private readonly IApiClient<ISpecialistApi> _apiClient;

        public SpecialistsServiceApi(IApiClient<ISpecialistApi> apiClient)
        {
            _apiClient = apiClient;
        }
        public IObservable<IEnumerable<SpecialistDto>> GetSpecialist()
        {
          return  RemoteRequestObservableAsync<IEnumerable<SpecialistDto>>(_apiClient.Client.GetSpecialists())
                .Select(e => e.Result);
        }

        public IObservable<IEnumerable<SpecialityDto>> GetSpecialities()
        {
            return  RemoteRequestObservableAsync<IEnumerable<SpecialityDto>>(_apiClient.Client.GetSpecialities())
                .Select(e => e.Result);
        }

        public IObservable<SpecialistDto> GetSpecialistById(string id)
        {
            throw new NotImplementedException();
        }
    }
}