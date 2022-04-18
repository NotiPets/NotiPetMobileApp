using System;
using System.Collections.Generic;
using NotiPet.Data.Dtos;

namespace NotiPet.Data.Services
{
    public interface IAssetServiceApi
    {
        public IObservable<IEnumerable<AssetServiceDto>>  GetAllProducts();
        IObservable<IEnumerable<AssetServiceDto>> GetServicesByBusinessId(int id);

    }
}