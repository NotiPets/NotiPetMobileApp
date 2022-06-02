

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using NotiPet.Data.Dtos;
using NotiPet.Data.Services;
using NotiPet.Domain.Models;
using NotiPet.Mocks.Dtos;

namespace NotiPet.Mocks.Services
{
    public class SpecialistsServiceApiMock:ISpecialistServiceApi
    {
        private IEnumerable<SpecialistDto> _specialistDtos;
        public SpecialistsServiceApiMock()
        {
            _specialistDtos = new SpecialistsDtoGenerator().Specialists;
        }
        public IObservable<IEnumerable<SpecialistDto>> GetSpecialist()
        {
            return Observable.Return(_specialistDtos);
        }

        public IObservable<IEnumerable<SpecialityDto>> GetSpecialities()
        {
            return Observable.Return(new SpecialityDtoGenerator().SpecialistDtos);
        }

        public IObservable<SpecialistDto> GetSpecialistById(string Id)
        {
            var a = _specialistDtos.FirstOrDefault(x => x.User.Id == Id);
           return Observable.Return(a);
        }
    }
}