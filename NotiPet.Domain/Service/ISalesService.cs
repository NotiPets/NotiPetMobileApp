using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        void ReceiveMessage(Action<string> GetMessageAndUser);
         Task Disconnect();
         Task Connect();
    }
}