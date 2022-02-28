using System.Collections.Generic;
using Bogus;
using NotiPet.Data.Dtos;

namespace NotiPet.Mocks.Services
{
    public class PetDtoGenerator
    {
        public IEnumerable<PetDto> Veterinaries { get; set; }

        private PetTypeDto[] petType = new[]
        {
            new PetTypeDto()
            {
                Id = 0,
                Name = "Labrador Retriever"
            },
            new PetTypeDto()
            {
                Id = 1,
                Name = "German Shorthaired Pointer"
            },
            new PetTypeDto()
            {
                Id = 2,
                Name = "Cocker Spaniel"
            },
            new PetTypeDto()
            {
                Id = 3,
                Name = "Nederlandse Kooikerhondje"
            },
        };

        public PetDtoGenerator()
        {
            var pet = new Faker<PetDto>()
                .RuleFor(e => e.UserId, x => 1)
                .RuleFor(e => e.PictureUrl, x => x.Image.LoremFlickrUrl(keywords: "dog"))
                .RuleFor(e => e.Description, x => x.Commerce.ProductDescription())
                .RuleFor(e => e.BirthDate, x => x.Date.Past())
                .RuleFor(e => e.Name, x => x.Person.FirstName)
                .RuleFor(e => e.PetTypeId, x => petType[x.Random.Int(0,3)].Id)
                .RuleFor(e => e.PetType, x => petType[x.Random.Int(0,3)])
                .RuleFor(e => e.Create, x => x.Date.Past());
            Veterinaries = pet.Generate(10);
            
        }
    }
}