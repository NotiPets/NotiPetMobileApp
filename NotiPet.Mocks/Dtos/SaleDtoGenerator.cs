using System;
using System.Collections;
using System.Collections.Generic;
using Bogus;
using NotiPet.Data.Dtos;
using NotiPet.Mocks.Services;

namespace NotiPet.Mocks.Dtos
{
    public class SaleDtoGenerator
    {
        public IEnumerable<SaleDto> SalesDto { get; set; }
        public SaleDtoGenerator()
        {
            var appointment = new AppointmentDtoGenerator();
            var sale = new Faker<OrderDto>()
                .RuleFor(e=>e.Appointment, x=>x.PickRandom(appointment.AppointmentDtos) )
                .RuleFor(e=>e.UserId, UserDtoGenerator.GenerateUser().Id)
                .RuleFor(x=>x.Quantity,x=>1);
            SalesDto = new Faker<SaleDto>()
                .RuleFor(x => x.Orders, sale.Generate(5))
                .RuleFor(x => x.Total, x => x.Random.Double())
                .RuleFor(e => e.Created, DateTime.Now).Generate(5);
        }
        
    }
}