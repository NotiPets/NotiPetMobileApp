using System;
using System.Collections.Generic;
using System.Reactive;
using DynamicData;
using NotiPet.Domain.Models;

namespace NotiPet.Domain.Service
{
    public interface IPetsService
    {
        IObservable<IEnumerable<Pet>> GetPets(string userId);
        IObservable<Pet> SavePet(CreatePetModel createPetModel);
        SourceCache<Pet, string> Pets { get; }
        Func<Pet, bool> SearchPredicate(string text);
        public List<PetInformation> PetInformations { get;  }
        public List<PetType> PetTypes { get;  }
        public List<PetSize> PetSizes { get;  }
        public IObservable<object> RemovePet(string id);
        IObservable<Pet> EditPet(CreatePetModel pet);
        IObservable<IEnumerable<Vaccinate>> GetVaccinesByPet(string petId);
        SourceList<Vaccinate> Vaccinate { get; }
    }
}