using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using NotiPet.Data.Dtos;

namespace NotiPet.Data.Services
{
    public class AssetServiceApi:BaseApiService,IAssetServiceApi
    {
        private readonly IApiClient<IAssetApi> _apiClient;

        public AssetServiceApi(IApiClient<IAssetApi> apiClient)
        {
            _apiClient = apiClient;
        }
        public IObservable<IEnumerable<AssetServiceDto>> GetAllProducts()
        {
           return RemoteRequestObservableAsync<IEnumerable<AssetServiceDto>>(_apiClient.Client.GetAssetService(),false)
                .Select(e => e.Result);
        }
        public IObservable<IEnumerable<AssetServiceDto>> GetServicesByBusinessId(int id)
        {
            return RemoteRequestObservableAsync<IEnumerable<AssetServiceDto>>(_apiClient.Client.GetAssetServiceByBusinessId(id),false)
                .Select(e => e.Result);
        }
       

    }
}