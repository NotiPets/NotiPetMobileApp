using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using NotiPet.Data.Dtos;
using NotiPet.Data.Services;
using NotiPet.Domain.Models;
using NotiPet.Mocks.Dtos;

namespace NotiPet.Mocks.Services
{
    public class SalesServiceApiMock:ISalesServiceApi
    {
        public IObservable<IEnumerable<SaleDto>> GetSaleByUserId(string userId)
        {
           return Observable.Return(new SaleDtoGenerator().SalesDto);
        }

        public IObservable<SaleDto> PostSale(RequestOrderDto requestOrderDto)
        {
            return Observable.Return(new SaleDtoGenerator().SalesDto.FirstOrDefault());
        }

        public IObservable<IEnumerable<AppointmentDto>> GetAppointmentByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public IObservable<bool> CancelOrder(string id)
        {
            throw new NotImplementedException();
        }

        public IObservable<AppointmentDto> UpdateAppointment(AppointmentDto appointmentSaleAppointment)
        {
            throw new NotImplementedException();
        }
    }
}