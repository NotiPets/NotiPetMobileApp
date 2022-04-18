using System;
using System.Net.Http;
using System.Threading.Tasks;
using NotiPet.Data.Dtos;
using Refit;

namespace NotiPet.Data
{
    public interface IUserApi
    {
        [Post("/LogIn")]
        IObservable<HttpResponseMessage> LogIn([Body]RequestAuthenticationDto requestAuthenticationDto);
        [Post("/SignUp")]
        IObservable<HttpResponseMessage> SignUp([Body]UserDto userRole);

        [Get("/Users/{username}")]
        IObservable<HttpResponseMessage> GetUserById(string username);
        [Put("/Users/userId")]
        IObservable<HttpResponseMessage> UpdateUser(string userId, [Body]UserDto user);
    }
}