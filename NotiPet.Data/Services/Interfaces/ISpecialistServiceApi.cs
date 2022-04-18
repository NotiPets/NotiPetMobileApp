using System;
using System.Collections.Generic;
using DynamicData;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;

namespace NotiPet.Data.Services
{
    public interface ISpecialistServiceApi
    {
        public IObservable<IEnumerable<SpecialistDto>> GetSpecialist();
        IObservable<IEnumerable<SpecialityDto>> GetSpecialities();
        IObservable<SpecialistDto> GetSpecialistById(string id);
        
    }
}