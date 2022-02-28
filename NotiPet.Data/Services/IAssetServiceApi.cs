using System;
using System.Collections.Generic;
using NotiPet.Data.Dtos;

namespace NotiPet.Data.Services
{
    public interface IAssetServiceApi
    {
        public IObservable<IEnumerable<AssetServiceDto>>  GetAllProducts();
        public IObservable<AssetServiceDto> GetProduct(string productName);
        public IObservable<IEnumerable<AssetServiceDto>> GetProductByProductName(string productName);
        public IObservable<IEnumerable<AssetServiceTypeDto>> GetAssetsTypes();
    }
}