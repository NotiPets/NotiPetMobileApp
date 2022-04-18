using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using AutoMapper;
using DynamicData;
using DynamicData.Binding;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;

namespace NotiPet.Data.Services
{
    public class SpecialistsService:ISpecialistsService
    {
        private readonly IMapper _mapper;
        private readonly ISpecialistServiceApi _serviceApi;
        public SourceCache<Specialist, int> SpecialistSource => _specialistSource;
        private readonly SourceCache<Specialist, int> _specialistSource = new(x => x.Id);
        private readonly SourceCache<Speciality, int> _specialitySource = new(x => x.Id);

        public SpecialistsService(IMapper mapper,ISpecialistServiceApi serviceApi)
        {
            _mapper = mapper;
            _serviceApi = serviceApi;
        }
        public IObservable<IEnumerable<Specialist>> GetSpecialists()
        {
            return _serviceApi.GetSpecialist()
                 .Select(_mapper.Map<List<Specialist>>)
                .Do(_specialistSource.AddOrUpdate);
        }

        public IObservable<IEnumerable<Speciality>> GetSpecialities()
        {
            return _serviceApi.GetSpecialities()
                .Select(_mapper.Map<IEnumerable<Speciality>>)
                .Do(_specialitySource.AddOrUpdate);
        }

        private readonly SourceCache<ParameterOption, int> _parametersOptions =
            new SourceCache<ParameterOption, int>(x=>x.Id);
        public SourceCache<ParameterOption, int> ParametersOptions => _parametersOptions;
        public SourceCache<Speciality, int> SpecialitySource => _specialitySource;

        public IObservable<IEnumerable<ParameterOption>> ParameterOptions()
        {
            var parameters = new List<ParameterOption>()
            {
                
                new ParameterOption("Speciality",false,true,3,"Sort")
                    .SetSortExpression<Specialist>(SortExpressionComparer<Specialist>.Ascending(e=>e.Speciality?.Name)),
                
                new ParameterOption("Name",false,true,4,"Sort")
                    .SetSortExpression<Specialist>(SortExpressionComparer<Specialist>.Ascending(e=>e.User.Names)),
            };
            return Observable.Return(parameters).Do(_parametersOptions.AddOrUpdate);
        }

        public IObservable<Specialist> GetSpecialistById(string id)
        {
            return _serviceApi.GetSpecialistById(id)
                .Select(_mapper.Map<Specialist>);
        }
    }
}