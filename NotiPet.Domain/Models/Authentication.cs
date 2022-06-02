using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NotiPet.Domain.Models
{
    public class Authentication
    {
        public Authentication(string jwt, User user)
        {
            Jwt = jwt;
            User = user;
        }


        public string Jwt { get; set; }

        public User User { get; set; }
    }
    public interface IAuthenticationRequestViewModel
    {
        public string Password { get; set; }
        public string Username { get; set; }

    }
}