using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using AutoMapper;
using DynamicData;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;

namespace NotiPet.Data.Services
{
    public class SalesService:ISalesService
    {
        private readonly IMapper _mapper;
        private readonly ISalesServiceApi _salesServiceApi;
        public SourceCache<Sales, string> DataSource => _dataSource;
        public SourceCache<Appointment, string> AppointmentDatasource => _appointmentDatasource;
        private SourceCache<Sales, string> _dataSource = new SourceCache<Sales, string>(x=>x.Id);
        private SourceCache<Appointment, string> _appointmentDatasource = new SourceCache<Appointment, string>(x=>x.Id);
        public SalesService(IMapper mapper,ISalesServiceApi salesServiceApi)
        {
            _mapper = mapper;
            _salesServiceApi = salesServiceApi;
        }
        public IObservable<IEnumerable<Sales>> GetSaleByUserId(string userId)
        {
            return _salesServiceApi.GetSaleByUserId(userId).Select(_mapper.Map<IEnumerable<Sales>>)
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
                new(requestOrderDto.UserId,requestOrderDto.AssetServiceId,appointment,1,requestOrderDto.PetId)
            };
            return _salesServiceApi.PostSale(_mapper.Map<RequestOrderDto>(new RequestOrder(order,requestOrderDto.BusinessId)))
                .Select(_mapper.Map<Sales>);
        }

        public IObservable<IEnumerable<Appointment>> GetAppointmentByUserId(string userId)
        {
            return _salesServiceApi.GetAppointmentByUserId(userId).Select(_mapper.Map<IEnumerable<Appointment>>)
                .Do(_appointmentDatasource.AddOrUpdate);
        }
    }
}