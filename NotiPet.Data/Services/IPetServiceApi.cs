using System;
using System.Collections.Generic;
using NotiPet.Data.Dtos;

namespace NotiPet.Data.Services
{
    public interface IPetServiceApi
    {
        IObservable<IEnumerable<PetDto>> GetPets();

    }
}