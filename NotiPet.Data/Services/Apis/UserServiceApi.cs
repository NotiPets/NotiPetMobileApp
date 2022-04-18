using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;

namespace NotiPet.Data.Services
{
    public class UserServiceApi:BaseApiService,IUserServiceApi
    {
        private IApiClient<IUserApi> _apiClient;

        public UserServiceApi(IApiClient<IUserApi> apiClient)
        {
            _apiClient = apiClient;
        }

        public IObservable<AuthenticationDto> LogIn(RequestAuthenticationDto requestAuthenticationDto)
        {
            return RemoteRequestObservableAsync<AuthenticationDto>(_apiClient.Client.LogIn(requestAuthenticationDto))
                .Select(e=>e.Result);
        }

        public IObservable<JwtDto> SingUp(UserDto userRole)
        {
            return RemoteRequestObservableAsync<JwtDto>(_apiClient.Client.SignUp(userRole))
                .Select(e=>e.Result);
        }

        public IObservable<UserDto> GetUserById(string username)
        {
            return RemoteRequestObservableAsync<UserDto>(_apiClient.Client.GetUserById(username))
                .Select(e=>e.Result);
        }

        public IObservable<UserDto> UpdateUser(string id, UserDto user)
        {
            return RemoteRequestObservableAsync<UserDto>(_apiClient.Client.UpdateUser(id,user))
                .Select(e=>e.Result);
        }
    }
}