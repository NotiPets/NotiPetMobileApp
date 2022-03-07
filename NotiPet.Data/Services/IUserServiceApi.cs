using System;
using System.Collections.Generic;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;
using Refit;

namespace NotiPet.Data.Services
{
    public interface IUserServiceApi
    {
        IObservable<AuthenticationDto> LogIn(RequestAuthenticationDto requestAuthenticationDto);
         IObservable<JwtDto> SingUp(UserRoleDto userRole);
         
         IObservable<IEnumerable<UserRoleDto>> GetVeterinarians();

    }
}