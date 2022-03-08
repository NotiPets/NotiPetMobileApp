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
        IObservable<HttpResponseMessage> LogIn(RequestAuthenticationDto requestAuthenticationDto);
        [Post("/SignUp")]
        IObservable<HttpResponseMessage> SignUp(UserRoleDto userRole);
    }
}