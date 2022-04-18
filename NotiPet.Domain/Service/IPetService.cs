using System;
using System.Collections.Generic;
using DynamicData;
using NotiPet.Domain.Models;

namespace NotiPet.Domain.Service
{
    public interface IPetsService
    {
        IObservable<IEnumerable<Pet>> GetPets(string userId);
        SourceCache<Pet, string> Pets { get; }
        Func<Pet, bool> SearchPredicate(string text);
    }
}