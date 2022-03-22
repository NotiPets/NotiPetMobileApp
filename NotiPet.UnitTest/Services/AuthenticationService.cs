using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Reactive;
using Microsoft.Reactive.Testing;
using NotiPet.Data;
using NotiPet.Data.Services;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPet.Mocks.Services;
using NotiPet.UnitTest.Fixtures;
using NotiPet.UnitTest.Fixtures.User;
using NotiPetApp.Services;
using NotiPetApp.ViewModels;
using Xunit;

namespace NotiPet.UnitTest.Services
{
    public class AuthenticationServiceTest:BaseServiceTest
    {
        [Fact]
        public void CallLoginWithValidAccountShouldBeReturnAuthenticationModel()
        {
            var scheduler = new TestScheduler();
            var serviceFixture = new AuthenticationServiceFixture()
                .MapperWith(Mapper)
                .UserServiceApiServiceWith(new UserServiceApiMock());
            IAuthenticationService service =(AuthenticationService) serviceFixture;
            var fixtureViewModel = new LoginViewModelFixture()
                .AuthenticationWith(service)
                .SchedulerProviderWith(new SchedulerProvider(scheduler,scheduler));
            
            IAuthenticationRequestViewModel viewModel = (LoginViewModel)fixtureViewModel;
            viewModel.Username = "fiji1984@eiibps.com";
            viewModel.Password = "12345";
            var dto = new UserDtoGenerator().AuthenticationDto;
            Authentication expect = new AuthenticationFixture()
                .EmailWith(dto.Email)
                .PasswordWith(dto.Password)
                .TokenWith(dto.Token)
                .IsRegisterWith(dto.IsRegister);
            //ACT
           var result = service.Authentication(viewModel);
           using var observedResultSequence = result.Observe();
           
           //ASSET
           observedResultSequence.RecordedMessages.Should().AllSatisfy(x=>x.Should().NotBeNull());
        }

        //[Fact]
      
   
    }

      
}