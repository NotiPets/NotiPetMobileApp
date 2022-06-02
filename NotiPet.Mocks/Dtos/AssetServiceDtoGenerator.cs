using System.Collections.Generic;
using System.Linq;
using Bogus;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;

namespace NotiPet.Mocks
{
    public class AssetServiceDtoGenerator
    {
        public IEnumerable<AssetServiceDto> AssetServices { get; set; }
        
        public IEnumerable<AssetServiceTypeDto> AssetServicesTypes { get; set; }

        private int[] categories = new[]
        {
            1,
            2,
            3,
            4,
            5
        };
        private AssetServiceTypeDto[] type = new[]
        {
            new AssetServiceTypeDto()
            {
                  Id   = 1,
                  Description = "Bed"
            },
            new AssetServiceTypeDto()
            {
                Id   = 2,
                Description = "Toys"
            },
            new AssetServiceTypeDto()
            {
                Id   = 3,
                Description = "Food"
            },
            new AssetServiceTypeDto()
            {
                Id   = 4,
                Description = "Medication"
            },
        };
        public AssetServiceDtoGenerator()
        {
            var testUsers = new Faker<AssetServiceDto>()
                .RuleFor(x => x.Id, (f, u) => f.Random.Int())
                .RuleFor(x => x.Name, (f, u) => f.Commerce.ProductName())
                .RuleFor(x => x.Price, (f, u) => decimal.Parse(f.Commerce.Price()))
                .RuleFor(x => x.Quantity, (f, u) => f.Random.Int(1, 10))
                .RuleFor(x => x.Description, (f, u) => f.Commerce.ProductDescription())
                .RuleFor(x => x.Active, (f, u) => true)
                .RuleFor(x => x.Created, (f, u) => f.Date.Recent())
                .RuleFor(x => x.AssetsServiceType, (f, u) => categories[f.Random.Int(0, 1)]);
            AssetServicesTypes = type.ToList();
            AssetServices = testUsers.Generate(14000).ToList();
        }
    }
}