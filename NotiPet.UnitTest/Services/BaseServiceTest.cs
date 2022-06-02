using AutoMapper;
using NotiPet.Data.Mappers;

namespace NotiPet.UnitTest.Services
{
    public class BaseServiceTest
    {
        protected readonly IMapper Mapper;
        public BaseServiceTest()
        {
            Mapper = new Mapper(AutoMapperConfig.GetConfig());
        }
    }
}