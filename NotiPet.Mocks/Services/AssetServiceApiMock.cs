using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using NotiPet.Data.Dtos;
using NotiPet.Data.Services;
using NotiPet.Domain.Models;

namespace NotiPet.Mocks.Services
{
    public class AssetServiceApiMock:IAssetServiceApi
    {
        private readonly List<AssetServiceDto> _assetsServices;
        private readonly List<AssetServiceTypeDto> _assetServiceTypes;
        public AssetServiceApiMock()
        {
            var assetServices =  new AssetServiceDtoGenerator();
            _assetsServices = assetServices.AssetServices.ToList();
            _assetServiceTypes = assetServices.AssetServicesTypes.ToList();
        }

        public IObservable<IEnumerable<AssetServiceDto>> GetAllProducts(int veterinary)
            => Observable.Return(_assetsServices).Delay(TimeSpan.FromSeconds(1));

        public IObservable<IEnumerable<AssetServiceDto>> GetServicesByBusinessId(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<AssetServiceDto> GetProduct(string productName)
            => Observable.Return(_assetsServices.FirstOrDefault(e=>e.Name == productName))
                .Delay(TimeSpan.FromSeconds(5));

        public IObservable<IEnumerable<AssetServiceDto>> GetProductByProductName(string productName)       
            => Observable.Return(_assetsServices.Where(e=>e.Name.Contains(productName) ))
                .Delay(TimeSpan.FromSeconds(2));
        public IObservable<IEnumerable<AssetServiceTypeDto>> GetAssetsTypes()       
            => Observable.Return(_assetServiceTypes)
                .Delay(TimeSpan.FromSeconds(2));
    }
}