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
         IObservable<JwtDto> SingUp(UserDto userRole);


         IObservable<UserDto> GetUserById(string username);
    }
}