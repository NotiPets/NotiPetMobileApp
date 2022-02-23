using System;
using System.Collections.Generic;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;

namespace NotiPet.Data.Services
{
    public interface IUserServiceApi
    {
         IObservable<UserRoleDto> LogIn(string username,string password);
         IObservable<UserRoleDto> SingUp(UserRoleDto userRole);
         
         IObservable<IEnumerable<UserRoleDto>> GetVeterinarians();

    }
}