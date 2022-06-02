using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using NotiPet.Data.Dtos;
using NotiPet.Data.Services;
using NotiPet.Mocks.Dtos;

namespace NotiPet.Mocks.Services
{
    public class BusinessServiceApiMock:IBusinessServiceApi

    {
        private readonly BusinnessDtoGenerator _generator;
        public BusinessServiceApiMock()
        {
            _generator = new BusinnessDtoGenerator();
        }
        public IObservable<IEnumerable<BusinessDto>> GetBusiness()
             => Observable.Return(_generator.BusinessDtos);

        public IObservable<BusinessDto> GetBusiness(int id)
        {
            throw new NotImplementedException();
        }
    }
}