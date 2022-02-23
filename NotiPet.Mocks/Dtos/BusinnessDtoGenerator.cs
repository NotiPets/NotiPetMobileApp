using System.Collections.Generic;
using Bogus;
using NotiPet.Data.Dtos;

namespace NotiPet.Mocks.Dtos
{
    public class BusinnessDtoGenerator
    {
        public IEnumerable<BusinessDto> BusinessDtos { get; set; }
        public BusinnessDtoGenerator()
        {
            var business = new Faker<BusinessDto>()
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