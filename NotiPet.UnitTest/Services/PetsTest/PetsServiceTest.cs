using System;
using AutoMapper;
using FluentAssertions;
using NotiPet.Data.Services;
using NotiPet.Domain.Service;
using NotiPet.Mocks.Services;
using NotiPet.UnitTest.Fixtures;
using Xunit;

namespace NotiPet.UnitTest.Services.PetsTest
{
    public class PetsServiceTest:BaseServiceTest
    {
        private readonly IPetServiceApi _petServiceApi;
        public PetsServiceTest()
        {
            _petServiceApi = new PetServiceApiMock();
        }

        [Fact]
        public void GetPetsShouldBeReturnItemsGreaterThanZero()
        {
            var petServiceFixture = new PetsServiceFixture().MapperWith(Mapper)
                .PetServiceApiWith(_petServiceApi);
            //arrange
            IPetsService petService = (PetsService)petServiceFixture;
            
            //act
            petService.GetPets("test")
                .Subscribe();
            
            //assert
            petService.Pets.Items.Should().HaveCountGreaterThan(0);
            
        }
    }
}