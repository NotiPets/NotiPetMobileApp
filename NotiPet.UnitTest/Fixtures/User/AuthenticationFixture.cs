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

        private Authentication Build() => new Authentication(_token,_user);
        public AuthenticationFixture TokenWith(string token) => this.With(ref _token, token);

        public AuthenticationFixture EmailWith( Domain.Models.User user) => this.With(ref _user,user);

        
        private string _token;
        private Domain.Models.User _user ;

    }
}