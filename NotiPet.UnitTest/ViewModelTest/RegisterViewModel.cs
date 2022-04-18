using System;
using FluentAssertions;
using FluentAssertions.Reactive;
using NotiPet.Data.Services;
using NotiPet.Mocks.Services;
using NotiPet.UnitTest.Fixtures;
using NotiPetApp.Helpers;
using NotiPetApp.ViewModels;
using Xunit;

namespace NotiPet.UnitTest.ViewModelTest
{
    public class RegisterViewModelTest:BaseViewModelFixture
    {
        [Fact]
        public void ShouldBeReturnTokenWhenCallSingUp()
        {
            var fixture = new RegisterViewModelFixture()
                .AuthenticationWith(new AuthenticationService(new UserServiceApiMock(),Mapper));
            RegisterViewModel viewModel = fixture;
            viewModel.InitializeCommand.Execute().Subscribe();
            var userRole = new UserDtoGenerator();
            viewModel.Username = userRole.UserRoleDto.Username;
            viewModel.Name = userRole.UserRoleDto.Names;
            viewModel.LastName = userRole.UserRoleDto.Lastnames;
            viewModel.Email = userRole.UserRoleDto.Email;
            viewModel.Password = userRole.UserRoleDto.Password;
            viewModel.ConfirmPassword = userRole.UserRoleDto.Password;
            viewModel.City = userRole.UserRoleDto.City;
            viewModel.Province = userRole.UserRoleDto.Province;
            viewModel.Phone = userRole.UserRoleDto.Phone;
            viewModel.DocumentType = viewModel.DocumentTypes[0].DocumentId;
            viewModel.Document = userRole.UserRoleDto.Document;
            viewModel.Address1 = userRole.UserRoleDto.Address1;
            //ACT
            var result = viewModel.AuthenticationCommand.CanExecute;
            using var observedSequence = result.Observe();
            var resultCommand = viewModel.AuthenticationCommand.Execute();
            observedSequence.RecordedMessages.Should().Satisfy(canExecute => canExecute);
            using var observedCommandSequence = resultCommand.Observe();
           observedCommandSequence.RecordedMessages.Should().Satisfy(e => e!=null);
        }
        [Fact]
        public void ShouldBeExecuteTrueWhenCallSingUpAndAllFieldIsValid()
        {
            var fixture = new RegisterViewModelFixture()
                .AuthenticationWith(new AuthenticationService(new UserServiceApiMock(),Mapper));
            RegisterViewModel viewModel = fixture;
            viewModel.InitializeCommand.Execute().Subscribe();
            var userRole = new UserDtoGenerator();
            viewModel.Username = userRole.UserRoleDto.Username;
            viewModel.Name = userRole.UserRoleDto.Names;
            viewModel.LastName = userRole.UserRoleDto.Lastnames;
            viewModel.Email = userRole.UserRoleDto.Email;
            viewModel.Password = userRole.UserRoleDto.Password;
            viewModel.ConfirmPassword = userRole.UserRoleDto.Password;
            viewModel.City = userRole.UserRoleDto.City;
            viewModel.Province = userRole.UserRoleDto.Province;
            viewModel.Phone = userRole.UserRoleDto.Phone;
            viewModel.DocumentType = viewModel.DocumentTypes[0].DocumentId;
            viewModel.Document = userRole.UserRoleDto.Document;
            viewModel.Address1 = userRole.UserRoleDto.Address1;
            //ACT
            var result = viewModel.AuthenticationCommand.CanExecute;
            using var observedSequence = result.Observe();
            observedSequence.RecordedMessages.Should().Satisfy(canExecute => canExecute);

        }
        [Fact]
        public void ShouldNotBeEmptyTypesWhenInitializeCommand()
        {
            var fixture = new RegisterViewModelFixture()
                .AuthenticationWith(new AuthenticationService(new UserServiceApiMock(),Mapper));
            RegisterViewModel viewModel = fixture;
            viewModel.InitializeCommand.Execute().Subscribe();
            viewModel.DocumentTypes.Should().NotBeEmpty();
        }
    }
}