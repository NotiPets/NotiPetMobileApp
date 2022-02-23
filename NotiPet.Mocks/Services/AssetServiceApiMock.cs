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
        private readonly List<AssetServiceDto> _assetsServices;
        public AssetServiceApiMock()
        {
            _assetsServices = new AssetServiceDtoGenerator().AssetServices.ToList();
        }

        public IObservable<IEnumerable<AssetServiceDto>> GetAllProducts()
            => Observable.Return(_assetsServices).Delay(TimeSpan.FromSeconds(1));

        public IObservable<AssetServiceDto> GetProduct(string productName)
            => Observable.Return(_assetsServices.FirstOrDefault(e=>e.Name == productName))
                .Delay(TimeSpan.FromSeconds(5));

        public IObservable<IEnumerable<AssetServiceDto>> GetProductByProductName(string productName)       
            => Observable.Return(_assetsServices.Where(e=>e.Name.Contains(productName) ))
                .Delay(TimeSpan.FromSeconds(2));
    }
}