using System;
using System.Collections.Generic;
using DynamicData;
using NotiPet.Domain.Models;

namespace NotiPet.Domain.Service
{
    public interface ISpecialistsService
    {
        public SourceCache<Specialist,int> SpecialistSource { get; }
        IObservable<IEnumerable<Specialist>> GetSpecialists();
    }
}