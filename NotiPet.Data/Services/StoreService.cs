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
        private readonly IVeterinaryService _veterinaryService;

        private readonly SourceCache<AssetServiceModel, int> _assetSourceCache =
            new SourceCache<AssetServiceModel, int>(e => e.Id);

        private readonly SourceCache<ParameterOption, int> _parametersOptions =
            new SourceCache<ParameterOption, int>(x=>x.Id);



        public StoreService(IAssetServiceApi assetServiceApi,IMapper mapper,IVeterinaryService veterinaryService)
        {
            _assetServiceApi = assetServiceApi;
            _mapper = mapper;
            _veterinaryService = veterinaryService;
        }

        public SourceCache<AssetServiceModel, int> AssetsServices => _assetSourceCache;
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
                new ParameterOption($"All",true,false,1,"Filter")
                    .SetFilterExpression<AssetServiceModel>(e => true),
                new ParameterOption($"{AssetsServiceTypeId.Product}",false,false,1,"Filter")
                    .SetFilterExpression<AssetServiceModel>(e => e.AssetServiceType == AssetsServiceTypeId.Product),
                
                new ParameterOption($"{AssetsServiceTypeId.Service}",false,false,2,"Filter")
                    .SetFilterExpression<AssetServiceModel>(e => e.AssetServiceType == AssetsServiceTypeId.Service),
                
                new ParameterOption("Price",false,true,3,"Sort")
                    .SetSortExpression<AssetServiceModel>(SortExpressionComparer<AssetServiceModel>.Ascending(e=>e.Price)),
                
                new ParameterOption("Added Recently",false,true,4,"Sort")
                    .SetSortExpression<AssetServiceModel>(SortExpressionComparer<AssetServiceModel>.Ascending(e=>e.Created)),
            }; 
            
            return Observable.Return(parameters).Do(_parametersOptions.AddOrUpdate);
        }

        public IObservable<IEnumerable<AssetServiceModel>> GetServicesByBusinessId(int id)
        {
            return _assetServiceApi.GetServicesByBusinessId(id)
                .Select(e => _mapper.Map<IEnumerable<AssetServiceModel>>(e.Where(x=>x.AssetsServiceType == 1)));
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