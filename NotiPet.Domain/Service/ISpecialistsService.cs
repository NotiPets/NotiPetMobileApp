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
        IObservable<IEnumerable<Speciality>> GetSpecialities();
        public SourceCache<ParameterOption,int> ParametersOptions { get;  }
        public SourceCache<Speciality,int> SpecialitySource { get;  }
        public  IObservable<IEnumerable<ParameterOption>> ParameterOptions();
    }
}