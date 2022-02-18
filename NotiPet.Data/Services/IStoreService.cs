using System;
using System.Collections.Generic;
using DynamicData;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;

namespace NotiPet.Data.Services
{
    public interface IStoreService
    {
        public SourceCache<AssetServiceModel,Guid> AssetsServices { get;  }
        public IObservable<IEnumerable<AssetServiceModel>>  GetAllProducts();

        public List<ParameterOption> ParameterOptions();
        // public IObservable<AssetServiceDto> GetProduct(string productName);
        // public IObservable<IEnumerable<AssetServiceDto>> GetProductByProductName(string productName);
    }
}