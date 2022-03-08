using AutoMapper;
using NotiPet.Data.Services;
using NotiPet.Domain.Service;

namespace NotiPet.UnitTest.Fixtures
{
    public class AuthenticationServiceFixture:IBuilder
    {
        public static implicit operator AuthenticationService(AuthenticationServiceFixture fixture) => fixture.Build();

        private AuthenticationService Build() => new AuthenticationService(_authenticationService,_mapper);
        public AuthenticationServiceFixture UserServiceApiServiceWith(IUserServiceApi authenticationService) =>
            this.With(ref _authenticationService, authenticationService);
        private IUserServiceApi _authenticationService;
        public AuthenticationServiceFixture MapperWith(IMapper mapper) =>
            this.With(ref _mapper, mapper);
        private IMapper _mapper;
    }
}