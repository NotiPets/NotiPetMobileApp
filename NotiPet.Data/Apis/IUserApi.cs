using System;
using System.Net.Http;
using System.Threading.Tasks;
using NotiPet.Data.Dtos;
using Refit;

namespace NotiPet.Data
{
    public interface IUserApi
    {
        [Post("/LogIn/Mobile")]
        IObservable<HttpResponseMessage> LogIn([Body]RequestAuthenticationDto requestAuthenticationDto);
        [Post("/SignUp")]
        IObservable<HttpResponseMessage> SignUp([Body]UserDto userRole);
        
        [Get("/SignUp/ForgotPassword/{email}")]
        IObservable<HttpResponseMessage> ForgotPassword(string email);

        [Get("/Users/{username}")]
        IObservable<HttpResponseMessage> GetUserById(string username);
        [Put("/Users/userId")]
        IObservable<HttpResponseMessage> UpdateUser(string userId, [Body]UserDto user);
        [Get("/Users/ValidateCode/{code}")]
        IObservable<HttpResponseMessage> ValidateCode(int code);
        [Put("/Users/UpdatePassword/userId?userId={userId}&newPassword={newPassword}")]
        IObservable<HttpResponseMessage> UpdatePassword(string userId, string newPassword);
    }
}