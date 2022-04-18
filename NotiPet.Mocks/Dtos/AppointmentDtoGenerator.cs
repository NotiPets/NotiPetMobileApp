using System;
using System.Collections.Generic;
using Bogus;
using NotiPet.Data.Dtos;
using NotiPet.Mocks.Services;

namespace NotiPet.Mocks.Dtos
{
    public class AppointmentDtoGenerator
    {
        public List<AppointmentDto> AppointmentDtos { get; }

        public AppointmentDtoGenerator()
        {
            var specialist = SpecialistsDtoGenerator.GetSpecialist();
            var appointment = new Faker<AppointmentDto>()
                .RuleFor(e=>e.Active, true)
                .RuleFor(x=>x.IsEmergency,x=>x.Random.Bool())
                .RuleFor(x=>x.SpecialistId,x=>specialist.User.Id)
                .RuleFor(x=>x.Specialist,x=>specialist)
                .RuleFor(e=>e.AppointmentStatus,x=>x.Random.Int(0,2))
                .RuleFor(e=>e.Date,x=>DateTime.Now);
            AppointmentDtos = appointment.Generate(10);

        }
    }
}