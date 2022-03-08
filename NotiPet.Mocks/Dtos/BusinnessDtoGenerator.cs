using System;
using System.Collections.Generic;
using Bogus;
using NotiPet.Data.Dtos;

namespace NotiPet.Mocks.Dtos
{
    public class BusinnessDtoGenerator
    {
        public IEnumerable<BusinessDto> BusinessDtos { get; set; }

        private string[] companies = new string[10]
        {
            "Neurocardiove",
            "PET GOLD VETERINARIA",
            "ASM - Veterinaria",
            "Pili's Vet | Clínica Médica de Mascotas",
            "Veterinaria Pet Care",
            "Centro Veterinario Agrodiza Pet Shop",
            "Animed Hotel y Clínica Veterinaria",
            "Clínica Veterinaria UNPHU",
            "Clínica Veterinaria Dr. Cerda",
            "Malala's Veterinaria",



        };
        public BusinnessDtoGenerator()
        {
            var business = new Faker<BusinessDto>()
                .RuleFor(e=>e.Id,x=>Guid.NewGuid().ToString())
                .RuleFor(e => e.Name, x => x.Company.CompanyName())
                .RuleFor(e => e.City, x => x.Company.Locale)
                .RuleFor(e => e.Email, x => x.Person.Email)
                .RuleFor(e => e.City, x => x.Address.City())
                .RuleFor(e => e.Province, x => x.Address.State())
                .RuleFor(e => e.Longitude, x => x.Address.Longitude())
                .RuleFor(e => e.Latitude, x => x.Address.Latitude())
                .RuleFor(e=>e.PictureUrl, x=>x.Image.LoremFlickrUrl(keywords:"company"));
            BusinessDtos = business.Generate(10);
        }
    }
}