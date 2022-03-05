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
              => Observable.Return(generator.AuthenticationDto.Email.Equals(requestAuthenticationDto.Username)&&generator.AuthenticationDto.Password.Equals(requestAuthenticationDto.Password)?
                  generator.AuthenticationDto:null);

        public IObservable<UserRoleDto> SingUp(UserRoleDto userRole)
               => Observable.Return(generator.UserRoleDto);
        

         public IObservable<IEnumerable<UserRoleDto>> GetVeterinarians()
            => Observable.Return(generator.Veterinaries);
    }
}