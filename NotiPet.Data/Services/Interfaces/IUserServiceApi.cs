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
         IObservable<AuthenticationDto> SingUp(UserDto userRole);


         IObservable<UserDto> GetUserById(string username);
         IObservable<UserDto> UpdateUser(string id,UserDto user);
    }
}