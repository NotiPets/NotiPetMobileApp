using System;
using FluentAssertions;
using NotiPet.Data.Services;
using NotiPet.Domain.Service;
using NotiPet.Mocks.Services;
using NotiPet.UnitTest.Fixtures.VeterinaryTest;
using Xunit;

namespace NotiPet.UnitTest.Services.VeterinaryTest
{
    public class VeterinaryServiceTest:BaseServiceTest
    {
        [Fact]
        public void GetVeterinariesReturnItems()
        {
            var service = new VeterinaryServiceFixture()
                .MapperWith(Mapper)
                .BusinessServiceWith(new BusinessServiceApiMock());
            //ARRANGE
            IVeterinaryService vetService = (VeterinaryService)service;
        

            //ACT
            vetService.GetVeterinary()
                .Subscribe();
            //ASSET
            vetService.Veterinaries.Items.Should().NotBeEmpty();
        }
    }
}