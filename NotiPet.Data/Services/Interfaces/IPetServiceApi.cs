using System;
using System.Collections.Generic;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;

namespace NotiPet.Data.Services
{
    public interface IPetServiceApi
    {
        IObservable<IEnumerable<PetDto>> GetPets(string userId);
        IObservable<PetDto> SavePets(PetDto petDto);

        IObservable<object> RemovePets(string id);
        IObservable<PetDto> EditPet(PetDto map);
        IObservable<IEnumerable<DigitalVaccineDto>> GetVaccinesByPet(string petId);
        IObservable<VaccinatePdfDto>  GetVaccinePdf(string vaccinateId);
    }
}