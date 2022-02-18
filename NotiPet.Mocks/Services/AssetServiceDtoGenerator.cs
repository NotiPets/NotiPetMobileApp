using System.Collections.Generic;
using System.Linq;
using Bogus;
using NotiPet.Data.Dtos;

namespace NotiPet.Mocks
{
    public class AssetServiceDtoGenerator
    {
        public IEnumerable<AssetServiceDto> AssetServices { get; set; }

        public AssetServiceDtoGenerator()
        {
            var testUsers = new Faker<AssetServiceDto>()
                .RuleFor(x=>x.Id,(f,u)=>f.Random.Int())
                .RuleFor(x=>x.Name,(f,u)=>f.Commerce.ProductName())
                .RuleFor(x=>x.Price,(f,u)=>decimal.Parse(f.Commerce.Price()))
                .RuleFor(x=>x.Quantity,(f,u)=>f.Random.Int(1,10))
                .RuleFor(x=>x.Description,(f,u)=>f.Commerce.ProductDescription())
                .RuleFor(x=>x.Image,(f,u)=>f.Image.PicsumUrl())
                .RuleFor(x=>x.Active,(f,u)=>true);
            AssetServices = testUsers.Generate(14000).ToList();
        }
    }
}