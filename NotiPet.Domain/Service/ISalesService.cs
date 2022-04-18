using System;
using System.Collections.Generic;
using DynamicData;
using NotiPet.Domain.Models;

namespace NotiPet.Domain.Service
{
    public interface ISalesService
    {
        public SourceCache<Sales,string> DataSource { get;}
        public IObservable<IEnumerable<Sales>> GetSaleByUserId(string userId);
        public IObservable<Sales> PostSale(RequestOrder requestOrderDto);
        public IObservable<Sales> CreateAppointment(CreateAppointment requestOrderDto);
    }
}