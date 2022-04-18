using System;
using System.Collections.Generic;
using DynamicData;
using NotiPet.Domain.Models;

namespace NotiPet.Domain.Service
{
    public interface IVeterinaryService
    {
        public SourceCache<Veterinary, int> Veterinaries { get; }
        Func<Veterinary, bool> SearchPredicate(string text);
        IObservable<IEnumerable<Veterinary>> GetVeterinary();
        IObservable<Veterinary> GetVeterinary(int id);
        public SourceCache<ParameterOption,int> ParametersOptions { get;  }
        public  IObservable<IEnumerable<ParameterOption>> ParameterOptions();
    }
}