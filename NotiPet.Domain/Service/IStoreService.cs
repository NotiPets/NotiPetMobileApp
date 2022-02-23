using System;
using System.Collections.Generic;
using DynamicData;
using NotiPet.Domain.Models;

namespace NotiPet.Domain.Service
{
    public interface IStoreService
    {
        public SourceCache<AssetServiceModel,Guid> AssetsServices { get;  }
        public SourceCache<ParameterOption,string> ParametersOptions { get;  }
        public IObservable<IEnumerable<AssetServiceModel>>  GetAllProducts();

        public  IObservable<IEnumerable<ParameterOption>> ParameterOptions();

        // public IObservable<AssetServiceDto> GetProduct(string productName);
        // public IObservable<IEnumerable<AssetServiceDto>> GetProductByProductName(string productName);
    }
}