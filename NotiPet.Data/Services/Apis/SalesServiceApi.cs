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

        public IObservable<IEnumerable<AppointmentDto>> GetAppointmentByUserId(string userId)
        {
            return RemoteRequestObservableAsync<IEnumerable<AppointmentDto>>(_apiClient.Client.GetAppointmentByUser(userId)).Select(e=>e.Result);
        }

        public IObservable<bool> CancelOrder(string id)
        {
            return RemoteRequestObservableAsync<object>(_apiClient.Client.CancelOrder(id),false)
                .Select(e=>e.Result!=null);
        }

        public IObservable<AppointmentDto> UpdateAppointment(AppointmentDto appointmentSaleAppointment)
        {
            return RemoteRequestObservableAsync<AppointmentDto>(_apiClient.Client.UpdateAppointment(appointmentSaleAppointment.Id,appointmentSaleAppointment),true)
                .Select(e=>e.Result);
        }
    }
}