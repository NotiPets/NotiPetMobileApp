using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive.Linq;
using NotiPet.Data.Dtos;
using NotiPet.Data.Services;
using NotiPet.Domain.Models;

namespace NotiPet.Mocks.Services
{
    public class PetServiceApiMock:IPetServiceApi
    {
        private readonly IEnumerable<PetDto> petDtos;
        public PetServiceApiMock()
        {
            petDtos = new PetDtoGenerator().Veterinaries;
        }

        public IObservable<IEnumerable<PetDto>> GetPets(string userId)
        {
           return Observable.Return(petDtos);
        }

        public IObservable<PetDto> SavePets(PetDto petDto)
        {
            throw new NotImplementedException();
        }

        
    }
}