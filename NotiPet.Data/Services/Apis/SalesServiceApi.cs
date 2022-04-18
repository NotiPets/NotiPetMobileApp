using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using NotiPet.Data.Dtos;

namespace NotiPet.Data.Services
{
    public class SalesServiceApi:BaseApiService,ISalesServiceApi
    {
        private readonly IApiClient<ISalesApi> _apiClient;

        public SalesServiceApi(IApiClient<ISalesApi> apiClient)
        {
            _apiClient = apiClient;
        }
        public IObservable<IEnumerable<SaleDto>> GetSaleByUserId(string userId)
        {
            return RemoteRequestObservableAsync<IEnumerable<SaleDto>>(_apiClient.Client.GetSalesOrder(userId)).Select(e=>e.Result);
        }

        public IObservable<SaleDto> PostSale(RequestOrderDto requestOrderDto)
        {
            return RemoteRequestObservableAsync<SaleDto>(_apiClient.Client.PostSalesOrder(requestOrderDto)).Select(e=>e.Result);
        }
    }
}