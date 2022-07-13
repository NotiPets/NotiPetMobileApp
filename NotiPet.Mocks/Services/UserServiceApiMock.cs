using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using NotiPet.Data.Dtos;
using NotiPet.Data.Services;
using NotiPet.Domain.Models;

namespace NotiPet.Mocks.Services
{
    public class UserServiceApiMock:IUserServiceApi
    {

        private readonly UserDtoGenerator generator;
        public UserServiceApiMock()
        {
            this.generator = new UserDtoGenerator();

        }

        public IObservable<AuthenticationDto> LogIn(RequestAuthenticationDto requestAuthenticationDto)
              => Observable.Return(generator.AuthenticationDto.User.Username.Equals(requestAuthenticationDto.Username)&&generator.AuthenticationDto.User.Password.Equals(requestAuthenticationDto.Password)?
                  generator.AuthenticationDto:null);

        public IObservable<AuthenticationDto> SingUp(UserDto userRole)
               => Observable.Return(new AuthenticationDto());

        public IObservable<UserDto> GetUserById(string username)
        {
            return  Observable.Return(generator.UserRoleDto);
        }

        public IObservable<UserDto> UpdateUser(string id, UserDto user)
        {
            if (  generator.UserRoleDto.Id != user.Id)
            {
                throw new NullReferenceException();
            }

            generator.UserRoleDto = user;
            return  Observable.Return(generator.UserRoleDto);
        }

        public IObservable<bool> ForgotPassword(string email)
        {
            throw new NotImplementedException();
        }

        public IObservable<UserDto> ValidateCode(int code)
        {
            throw new NotImplementedException();
        }

        public IObservable<bool> UpdatePassword(string userId, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}