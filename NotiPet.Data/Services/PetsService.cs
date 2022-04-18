using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using AutoMapper;
using DynamicData;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;

namespace NotiPet.Data.Services
{
    public class PetsService:IPetsService
    {
        private readonly IPetServiceApi _petService;
        private readonly IMapper _mapper;
        private SourceCache<Pet, string> _petSource = new SourceCache<Pet, string>(e => e.Id);
        public PetsService(IPetServiceApi petService, IMapper mapper)
        {
            _petService = petService;
            _mapper = mapper;
        }


        public IObservable<IEnumerable<Pet>> GetPets(string userId)
        {
            return _petService.GetPets(userId)
                .Select(_mapper.Map<IEnumerable<Pet>>)
                .Do(_petSource.AddOrUpdate);
        }

        public SourceCache<Pet, string> Pets => _petSource;

        public Func<Pet, bool> SearchPredicate(string text) =>
            pet => string.IsNullOrEmpty(text)|| (pet.Name.Contains(text));
    }
}