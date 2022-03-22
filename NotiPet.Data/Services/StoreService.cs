using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using AutoMapper;
using DynamicData;
using NotiPet.Domain.Models;
using DynamicData;
using DynamicData.Binding;
using ImTools;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Service;

namespace NotiPet.Data.Services
{
    public class StoreService : IStoreService, IDisposable
    {
        private readonly IAssetServiceApi _assetServiceApi;
        private readonly IMapper _mapper;

        private readonly SourceCache<AssetServiceModel, Guid> _assetSourceCache =
            new SourceCache<AssetServiceModel, Guid>(e => e.Guid);

        private readonly SourceCache<ParameterOption, int> _parametersOptions =
            new SourceCache<ParameterOption, int>(x=>x.Id);



        public StoreService(IAssetServiceApi assetServiceApi,IMapper mapper)
        {
            _assetServiceApi = assetServiceApi;
            _mapper = mapper;
        }

        public SourceCache<AssetServiceModel, Guid> AssetsServices => _assetSourceCache;
        public SourceCache<ParameterOption, int> ParametersOptions => _parametersOptions;


        public IObservable<IEnumerable<AssetServiceModel>> GetAllProducts()

        {
            _assetSourceCache.Clear();
         return   _assetServiceApi.GetAllProducts()
             .Select(e=>_mapper.Map<List<AssetServiceModel>>(e))
                .Do(_assetSourceCache.AddOrUpdate);
        }

        public IObservable<IEnumerable<ParameterOption>> ParameterOptions()
        {
            var parameters = new List<ParameterOption>()
            {
                new ParameterOption("Bed",false,false,1,"Filter")
                    .SetFilterExpression<AssetServiceModel>(e => e.AssetServiceType.Description == "Toys"),
                
                new ParameterOption("Toys",false,false,2,"Filter")
                    .SetFilterExpression<AssetServiceModel>(e => e.AssetServiceType.Description  == "Bed"),
                
                new ParameterOption("Price",false,true,3,"Sort")
                    .SetSortExpression<AssetServiceModel>(SortExpressionComparer<AssetServiceModel>.Ascending(e=>e.Price)),
                
                new ParameterOption("Added Recently",false,true,4,"Sort")
                    .SetSortExpression<AssetServiceModel>(SortExpressionComparer<AssetServiceModel>.Ascending(e=>e.Created)),
            };
            return Observable.Return(parameters).Do(_parametersOptions.AddOrUpdate);
        }
        
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