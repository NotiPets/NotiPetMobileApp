using System;
using System.Collections.Generic;
using DynamicData;
using NotiPet.Domain.Models;

namespace NotiPet.Domain.Service
{
    public interface IStoreService
    {
        public SourceCache<AssetServiceModel,Guid> AssetsServices { get;  }
        public SourceCache<ParameterOption,int> ParametersOptions { get;  }
        public IObservable<IEnumerable<AssetServiceModel>>  GetAllProducts();

        public  IObservable<IEnumerable<ParameterOption>> ParameterOptions();

    }
}