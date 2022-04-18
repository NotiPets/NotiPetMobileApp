using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using AutoMapper;
using DynamicData;
using DynamicData.Binding;
using ImTools;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;

namespace NotiPet.Data.Services
{
    public class VeterinaryService : IVeterinaryService

    {
        private SourceCache<Veterinary, int> _sourceCache = new SourceCache<Veterinary, int>(e => e.Id);
        private readonly IBusinessServiceApi _businessService;
        private readonly IMapper _mapper;

        public VeterinaryService(IBusinessServiceApi businessService, IMapper mapper)
        {
            _businessService = businessService;
            _mapper = mapper;
        }

        public SourceCache<Veterinary, int> Veterinaries => _sourceCache;

        public Func<Veterinary, bool> SearchPredicate(string text) => vet =>
            string.IsNullOrEmpty(text) || vet.Name.ToLower().Contains(text.ToLower());

        public IObservable<IEnumerable<Veterinary>> GetVeterinary()
            => _businessService.GetBusiness().Select(_mapper.Map<IEnumerable<Veterinary>>)
                .Do(_sourceCache.AddOrUpdate);

        public IObservable<Veterinary> GetVeterinary(int id)
  
            => _businessService.GetBusiness(id).Select(_mapper.Map<Veterinary>)
                .Do(_sourceCache.AddOrUpdate);
       

        private readonly SourceCache<ParameterOption, int> _parametersOptions =
            new SourceCache<ParameterOption, int>(x => x.Id);

        public SourceCache<ParameterOption, int> ParametersOptions => _parametersOptions;

        public IObservable<IEnumerable<ParameterOption>> ParameterOptions()
        {
            var parameters = new List<ParameterOption>()
            {

                new ParameterOption("Name", false, true, 3, "Sort")
                    .SetSortExpression(SortExpressionComparer<Veterinary>.Ascending(e => e.Name)),

                new ParameterOption("Province", false, true, 4, "Sort")
                    .SetSortExpression(SortExpressionComparer<Veterinary>.Ascending(e => e.Province)),
            };
            return Observable.Return(parameters).Do(_parametersOptions.AddOrUpdate);
        }
    }
}