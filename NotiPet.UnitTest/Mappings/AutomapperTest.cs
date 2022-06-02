using AutoMapper;
using NotiPet.Data.Mappers;
using Xunit;

namespace NotiPet.UnitTest.Mappings
{
    public class AutomapperTest
    {
        public AutomapperTest()
        {
            _autoMapperConfig = AutoMapperConfig.GetConfig();
        }

        [Fact]
        public void ValidateAutoMapperProfiles()
        {
            _autoMapperConfig.AssertConfigurationIsValid();
        }

        private MapperConfiguration _autoMapperConfig;
    }
}