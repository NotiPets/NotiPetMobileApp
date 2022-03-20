using System;
using System.Collections.Generic;
using DynamicData;
using NotiPet.Domain.Models;

namespace NotiPet.Domain.Service
{
    public interface IVeterinaryService
    {
        public SourceCache<Veterinary, string> Veterinaries { get; }
        Func<Veterinary, bool> SearchPredicate(string text);
        IObservable<IEnumerable<Veterinary>> GetVeterinary();
        public SourceCache<ParameterOption,int> ParametersOptions { get;  }
        public  IObservable<IEnumerable<ParameterOption>> ParameterOptions();
    }
}