using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using NotiPet.Data.Dtos;
using NotiPet.Data.Services;

namespace NotiPet.Mocks.Services
{
    public class AssetServiceApiMock:IAssetServiceApi
    {
        public List<AssetServiceDto> AssetsServices  { get; set; }
        public AssetServiceApiMock()
        {
            //AssetsServices = new AssetServiceDtoGenerator().AssetServices.ToList();
        }

        public IObservable<IEnumerable<AssetServiceDto>> GetAllProducts()
            => Observable.Return(new AssetServiceDtoGenerator().AssetServices.ToList()).Delay(TimeSpan.FromSeconds(1));

        public IObservable<AssetServiceDto> GetProduct(string productName)
            => Observable.Return(AssetsServices.FirstOrDefault(e=>e.Name == productName))
                .Delay(TimeSpan.FromSeconds(5));

        public IObservable<IEnumerable<AssetServiceDto>> GetProductByProductName(string productName)       
            => Observable.Return(AssetsServices.Where(e=>e.Name.Contains(productName) ))
                .Delay(TimeSpan.FromSeconds(2));
    }
}