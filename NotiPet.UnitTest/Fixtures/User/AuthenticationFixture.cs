using Newtonsoft.Json;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;

namespace NotiPet.UnitTest.Fixtures.User
{
    public class AuthenticationFixture:IBuilder
    {
        public static implicit operator Authentication(AuthenticationFixture authenticationFixture) =>
            authenticationFixture.Build();
        public static implicit operator AuthenticationDto(AuthenticationFixture authenticationFixture) =>
            authenticationFixture.BuildDto();

        private AuthenticationDto BuildDto() => JsonConvert.DeserializeObject<AuthenticationDto>("");

        private Authentication Build() => new Authentication(_token,_isRegister,_email,_password);
        public AuthenticationFixture TokenWith(string token) => this.With(ref _token, token);
        public AuthenticationFixture IsRegisterWith(bool register) => this.With(ref _isRegister,register);
        public AuthenticationFixture EmailWith(string email) => this.With(ref _email,email);
        public AuthenticationFixture PasswordWith(string password) => this.With(ref _password,password);
        
        private string _token;
        private bool _isRegister;
        private string _email ;
        private string _password;
    }
}