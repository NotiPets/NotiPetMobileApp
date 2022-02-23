using AutoMapper;
using NotiPet.Data.Services;
using NotiPet.Mocks.Services;

namespace NotiPet.UnitTest.Fixtures.User
{
    public class UserServiceFixture:IBuilder
    {
        public readonly IUserServiceApi UserServiceApi;
        public  IMapper Mapper;

        public UserServiceFixture()
        {
            UserServiceApi = new UserServiceApiMock();
        }

        public static implicit operator UserService(UserServiceFixture userServiceFixture) => userServiceFixture.Build();
        
        public UserServiceFixture WithMapper(IMapper mapper) =>
            this.With(ref Mapper, mapper);

        private  UserService Build() => new UserService(UserServiceApi,Mapper);
    }
}