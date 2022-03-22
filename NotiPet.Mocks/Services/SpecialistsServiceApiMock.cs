

using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using NotiPet.Data.Dtos;
using NotiPet.Data.Services;
using NotiPet.Mocks.Dtos;

namespace NotiPet.Mocks.Services
{
    public class SpecialistsServiceApiMock:ISpecialistServiceApi
    {

        public IObservable<IEnumerable<SpecialistDto>> GetSpecialist()
        {
            return Observable.Return(new SpecialistsDtoGenerator().Specialists);
        }
    }
}