using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using AutoMapper;
using DynamicData;
using NotiPet.Domain.Models;
using DynamicData;
using ImTools;
using NotiPet.Data.Dtos;

namespace NotiPet.Data.Services
{
    public class StoreService : IStoreService, IDisposable
    {
        private readonly IAssetServiceApi _assetServiceApi;
        private readonly IMapper _mapper;

        private readonly SourceCache<AssetServiceModel, Guid> _assetSourceCache =
            new SourceCache<AssetServiceModel, Guid>(e => e.Guid);

        public StoreService(IAssetServiceApi assetServiceApi,IMapper mapper)
        {
            _assetServiceApi = assetServiceApi;
            _mapper = mapper;
        }

        public SourceCache<AssetServiceModel, Guid> AssetsServices => _assetSourceCache;

        public IObservable<IEnumerable<AssetServiceModel>> GetAllProducts()

        {
         return   _assetServiceApi.GetAllProducts()
             .Select(e=>_mapper.Map<List<AssetServiceModel>>(e))
                .Do(_assetSourceCache.AddOrUpdate);
        }

        public List<ParameterOption> ParameterOptions()
        {
          return new List<ParameterOption>()
            {
                new ParameterOption()
                {
                    Title = "Casas",
                    IsActive = true

                },
                new ParameterOption()
                {
                    Title = "Juguetes",
                },
                new ParameterOption()
                {
                    Title = "Precio",
                    IsSort = true
                },
                new ParameterOption()
                {
                    Title = "Recientes",
                    IsSort = true,
                    IsActive = true
                },
               
            };
        }


        //
        // public IObservable<AssetServiceDto> GetProduct(string productName)
        //     => _assetServiceApi.GetProduct(productName)
        //         .Do(_assetSourceCache.AddOrUpdate);
        //
        // public IObservable<IEnumerable<AssetServiceDto>> GetProductByProductName(string productName)
        //     => _assetServiceApi.GetProductByProductName(productName)
        //         .Do(_assetSourceCache.AddOrUpdate);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _assetSourceCache?.Dispose();
            }
        }
    }
}