using System;
using System.Collections.Generic;
using System.Net.Http;
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

        public IObservable<AuthenticationDto> SingUp(UserDto userRole)
        {
            return RemoteRequestObservableAsync<AuthenticationDto>(_apiClient.Client.SignUp(userRole))
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

        public IObservable<bool> ForgotPassword(string email)
        {
            return RemoteRequestObservableAsync<object>(_apiClient.Client.ForgotPassword(email))
                .Select(e=>true);
        }

        public IObservable<UserDto> ValidateCode(int code)
        {
            return RemoteRequestObservableAsync<UserDto>(_apiClient.Client.ValidateCode(code))
                .Select(e=>e.Result);
        }

        public IObservable<bool> UpdatePassword(string userId, string newPassword)
        {
            return RemoteRequestObservableAsync<object>(_apiClient.Client.UpdatePassword(userId,newPassword),false)
                .Select(e=>e.SuccessResult );
        }
    }
}