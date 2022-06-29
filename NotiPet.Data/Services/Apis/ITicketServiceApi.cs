using System;
using System.Reactive.Linq;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;

namespace NotiPet.Data.Services
{
    public class TicketServiceApi:BaseApiService,ITicketServiceApi
    {
        private readonly IApiClient<ITicketApi> _apiClient;

        public TicketServiceApi(IApiClient<ITicketApi> apiClient)
        {
            _apiClient = apiClient;
        
        }

        public IObservable<TicketDto> SaveTicket(TicketDto ticket)
        {
            return RemoteRequestObservableAsync<TicketDto>(_apiClient.Client.CreateTicket(ticket))
                .Select(e=>e.Result);
        }
    }
}