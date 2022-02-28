using System;
using System.Collections.Generic;
using NotiPet.Data.Dtos;

namespace NotiPet.Data.Services
{
    public interface IBusinessServiceApi
    {
        IObservable<IEnumerable<BusinessDto>> GetBusiness();
    }
}