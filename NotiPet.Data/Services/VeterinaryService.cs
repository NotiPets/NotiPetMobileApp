using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using AutoMapper;
using DynamicData;
using ImTools;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;

namespace NotiPet.Data.Services
{
    public class VeterinaryService:IVeterinaryService

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
    public Func<Veterinary, bool> SearchPredicate(string text) => vet => string.IsNullOrEmpty(text)||vet.Name.ToLower().Contains(text.ToLower());

    public IObservable<IEnumerable<Veterinary>> GetVeterinary()
                    =>    _businessService.GetBusiness().Select(_mapper.Map<IEnumerable<Veterinary>>)
                                  .Do(_sourceCache.AddOrUpdate);
        
    }
}