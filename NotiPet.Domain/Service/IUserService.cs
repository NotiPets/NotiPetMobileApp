using System;
using System.Collections;
using System.Collections.Generic;
using DynamicData;
using NotiPet.Domain.Models;

namespace NotiPet.Domain.Service
{
    public interface IUserService
    {
        public  IObservable<User> GetUserById(string username);
    }
}