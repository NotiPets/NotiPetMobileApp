using System;
using System.Collections.Generic;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;
using Refit;

namespace NotiPet.Data.Services
{
    public interface ISalesServiceApi
    {
        public IObservable<IEnumerable<SaleDto>> GetSaleByUserId(string userId);
        public IObservable<SaleDto> PostSale([Body]RequestOrderDto requestOrderDto);
        public IObservable<IEnumerable<AppointmentDto>> GetAppointmentByUserId(string userId);
        IObservable<bool> CancelOrder(string id);
    }
}