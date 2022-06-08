using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DynamicData;
using Microsoft.AspNetCore.SignalR.Client;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;

namespace NotiPet.Data.Services
{
    public class SalesService:ISalesService
    {
        
        private readonly IMapper _mapper;
        private readonly ISalesServiceApi _salesServiceApi;
        private readonly HubConnection _hubConnection;
        public SourceCache<AppointmentSale, string> DataSource => _dataSource;
        public SourceCache<Appointment, string> AppointmentDatasource => _appointmentDatasource;
        private SourceCache<AppointmentSale, string> _dataSource = new SourceCache<AppointmentSale, string>(x=>x.OrderId);
        private SourceCache<Appointment, string> _appointmentDatasource = new SourceCache<Appointment, string>(x=>x.Id);
        public SalesService(IMapper mapper,ISalesServiceApi salesServiceApi,HubConnection hubConnection)
        {
            _mapper = mapper;
            _salesServiceApi = salesServiceApi;
            _hubConnection = hubConnection;
        }
        public IObservable<IEnumerable<AppointmentSale>> GetSaleByUserId(string userId)
        {
            
            return _salesServiceApi.GetSaleByUserId(userId)
                .Select(_mapper.Map<IEnumerable<Sales>>)
                .Select(e=>e.Select(x=>new AppointmentSale(x)))
                .Do(_dataSource.AddOrUpdate);
        }

        public IObservable<Sales> PostSale(RequestOrder requestOrderDto)
        {
            return _salesServiceApi.PostSale(_mapper.Map<RequestOrderDto>(requestOrderDto))
                .Select(_mapper.Map<Sales>);
        }

        public IObservable<Sales> CreateAppointment(CreateAppointment requestOrderDto)
        {
            var appointment = new Appointment(Guid.NewGuid().ToString(), null, null, EAppointmentStatus.Requested, requestOrderDto.IsEmergency,
                true, requestOrderDto.Date.ToUniversalTime(), DateTime.Now);
            var order = new List<Order>()
            {
                new(requestOrderDto.UserId,requestOrderDto.AssetServiceId.GetValueOrDefault(),appointment,1,requestOrderDto.PetId,null)
            };
            return _salesServiceApi.PostSale(_mapper.Map<RequestOrderDto>(new RequestOrder(order,requestOrderDto.BusinessId)))
                .Select(_mapper.Map<Sales>);
        }

        public IObservable<IEnumerable<Appointment>> GetAppointmentByUserId(string userId)
        {
            return _salesServiceApi.GetAppointmentByUserId(userId).Select(_mapper.Map<IEnumerable<Appointment>>)
                .Do(_appointmentDatasource.AddOrUpdate);
        }
        public async Task Connect()
        {
            await _hubConnection.StartAsync();
        }

        public IObservable<Appointment> UpdateAppointment(Appointment appointmentSaleAppointment)
        {
            return _salesServiceApi.UpdateAppointment(_mapper.Map<AppointmentDto>(appointmentSaleAppointment)).Select(_mapper.Map<Appointment>);
        }


        public async Task Disconnect()
        {
            await _hubConnection.StopAsync();
        }
        public void ReceiveMessage(Action<string> GetMessageAndUser)
        {
            _hubConnection.On("InformClient", GetMessageAndUser); 
        }
        public IObservable<bool> CancelOrder(string id)
        {
            return _salesServiceApi.CancelOrder(id);
        }
    }
}