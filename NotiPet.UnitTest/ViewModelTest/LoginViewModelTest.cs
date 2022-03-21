using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using FluentAssertions;
using FluentAssertions.Reactive;
using Microsoft.Reactive.Testing;
using NotiPet.Data.Services;
using NotiPet.Domain.Models;
using NotiPet.Mocks.Services;
using NotiPet.UnitTest.Fixtures;
using NotiPet.UnitTest.Fixtures.User;
using NotiPet.UnitTest.Services;
using NotiPetApp.Services;
using NotiPetApp.ViewModels;
using ReactiveUI.Validation.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace NotiPet.UnitTest.ViewModelTest
{
    public class LoginViewModelTest:BaseServiceTest
    {
        [Fact]
        public void CannotExecutingAuthenticationCommandShouldBeFalse_When_PasswordIsNull()
        {

            var scheduler= new TestScheduler();

            var viewModelFixture = new LoginViewModelFixture()
                .SchedulerProviderWith(new SchedulerProvider(scheduler,scheduler))
                .AuthenticationWith(new AuthenticationService(new UserServiceApiMock(),Mapper));

            //ARRANGE
            var viewModel = (LoginViewModel) viewModelFixture;
            viewModel.Username = "fiji1984@eiibps.com";
            viewModel.Password = null;
            //ACT
            var result = viewModel.AuthenticationCommand.CanExecute;
            using var observedSequence = result.Observe();
            //ASSET
            observedSequence.RecordedMessages.Should().Satisfy(canExecute => !canExecute);

        }
        [Fact]
        public void ShouldBeBusyWhenLoggingIn()
        {
            var scheduler= new TestScheduler();

            var viewModelFixture = new LoginViewModelFixture()
               .SchedulerProviderWith(new SchedulerProvider(scheduler,scheduler))
                .AuthenticationWith(new AuthenticationService(new UserServiceApiMock(),Mapper));

            var viewModel = (LoginViewModel) viewModelFixture;
            viewModel.Username = "fiji1984@eiibps.com";
            viewModel.Password =  "123";

            viewModel.IsBusy.Should().BeFalse();
            viewModel.AuthenticationCommand.Execute().Subscribe();
       
            scheduler.AdvanceBy(2);
            viewModel.IsBusy.Should().BeTrue();
            scheduler.AdvanceBy(2);
            viewModel.IsBusy.Should().BeFalse();

        }

        [Fact]
        public void Given_IsAuthenticated_When_ValidAccountIsUsedToLogin_Then_LoginWillSuccessfully()
        {


            var scheduler= new TestScheduler();

            var viewModelFixture = new LoginViewModelFixture()
                .SchedulerProviderWith(new SchedulerProvider(scheduler,scheduler))
                .AuthenticationWith(new AuthenticationService(new UserServiceApiMock(),Mapper));

            var viewModel = (LoginViewModel) viewModelFixture;
            //ARRANGE
            viewModel.Username = "fiji1984@eiibps.com";
            viewModel.Password = "12345";
            var canExecute = viewModel.IsValid();
            var result = viewModel.AuthenticationCommand.Execute();
            
            //ACT
            using var observedSequence = canExecute.Observe();
           
            using var observedResultSequence = result.Observe();
            //ASSERT
            observedSequence.RecordedMessages.Should().Satisfy(e => e);
            observedResultSequence.RecordedMessages.Should().Satisfy(e => !string.IsNullOrEmpty(e));

        }
        [Fact]
        public void Given_IsRegister_When_ForumAccountIsCompleted_Then_RegisterWillSuccessfully()
        {


            var viewModelFixture = new LoginViewModelFixture()
                .AuthenticationWith(new AuthenticationService(new UserServiceApiMock(),Mapper));
            var generator = new UserDtoGenerator();
            var viewModel = (LoginViewModel) viewModelFixture;
            //ARRANGE
            viewModel.IsRegister = true;
            viewModel.Username = generator.UserRoleDto.Username;
            viewModel.Password =  generator.UserRoleDto.Password;
            viewModel.Name =  generator.UserRoleDto.Names;
            viewModel.LastName = generator.UserRoleDto.Lastnames;
            viewModel.Address1 = generator.UserRoleDto.Address1;
            viewModel.Address2 = generator.UserRoleDto.Address2;
            viewModel.City = generator.UserRoleDto.City;
            viewModel.Phone = generator.UserRoleDto.Phone;
            viewModel.PersonalDocument = new PersonalDocument(generator.UserRoleDto.Document,generator.UserRoleDto.DocumentType);
            viewModel.Province = generator.UserRoleDto.Province;
            var canExecute = viewModel.IsValid();
            var result = viewModel.AuthenticationCommand.Execute();
            //ACT
            using var observedSequence = canExecute.Observe();
           
            using var observedResultSequence = result.Observe();
            //ASSERT
            observedSequence.RecordedMessages.Should().Satisfy(e => e);
            observedResultSequence.RecordedMessages.Should().Satisfy(e => !string.IsNullOrEmpty(e) );

        }
        [Fact]
        public void ShouldNotBeEmpty_ErrorMessage_When_LoginFail()
        {
            var scheduler= new TestScheduler();

            var viewModelFixture = new LoginViewModelFixture()
                .SchedulerProviderWith(new SchedulerProvider(scheduler,scheduler))
                .AuthenticationWith(new AuthenticationService(new UserServiceApiMock(),Mapper));

            var viewModel = (LoginViewModel) viewModelFixture;
            viewModel.Username = "fiji1@eiibps.com";
            viewModel.Password =  "151843";

            viewModel.IsBusy.Should().BeFalse();
            viewModel.AuthenticationCommand.Execute().Subscribe();
            viewModel.ErrorMessage.Should().NotBeEmpty();
 

        }
        [Fact]
        public void ShouldBeNull_ErrorMessage_When_LoginFail()
        {
            var scheduler= new TestScheduler();

            var viewModelFixture = new LoginViewModelFixture()
                .SchedulerProviderWith(new SchedulerProvider(scheduler,scheduler))
                .AuthenticationWith(new AuthenticationService(new UserServiceApiMock(),Mapper));

            var viewModel = (LoginViewModel) viewModelFixture;
            viewModel.Username = "fiji1984@eiibps.com";
            viewModel.Password = "12345";

            viewModel.AuthenticationCommand.Execute().Subscribe();
            viewModel.ErrorMessage.Should().BeNull();
 

        }
    }
    
}