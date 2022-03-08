using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NotiPet.Domain.Models
{
    public class Authentication
    {
        public Authentication(string token, bool isRegister, string email, string password)
        {
            Token = token;
            IsRegister = isRegister;
            Email = email;
            Password = password;
        }

        public string Token { get; }
        public bool IsRegister { get;  }
        public string Email { get; }
        public string Password { get; }
    }
    public interface IAuthenticationRequestViewModel
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

    }
}