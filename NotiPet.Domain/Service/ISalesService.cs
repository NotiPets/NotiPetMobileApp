using System;
using System.Collections.Generic;
using DynamicData;
using NotiPet.Domain.Models;

namespace NotiPet.Domain.Service
{
    public interface ISalesService
    {
        public SourceCache<AppointmentSale,string> DataSource { get;}
        public SourceCache<Appointment,string> AppointmentDatasource { get;}
        public IObservable<IEnumerable<AppointmentSale>> GetSaleByUserId(string userId);
        public IObservable<Sales> PostSale(RequestOrder requestOrderDto);
        public IObservable<Sales> CreateAppointment(CreateAppointment requestOrderDto);
        IObservable<IEnumerable<Appointment>> GetAppointmentByUserId(string userId);
        IObservable<bool> CancelOrder(string id);
    }
}