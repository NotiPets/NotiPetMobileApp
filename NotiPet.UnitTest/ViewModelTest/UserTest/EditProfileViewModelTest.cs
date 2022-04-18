using System;
using FluentAssertions;
using FluentAssertions.Reactive;
using Microsoft.Reactive.Testing;
using NotiPet.Data.Services;
using NotiPet.Mocks.Services;
using NotiPet.UnitTest.Fixtures.User;
using NotiPetApp.ViewModels;
using ReactiveUI.Testing;
using Xunit;

namespace NotiPet.UnitTest.ViewModelTest.UserTest
{
    public class EditProfileViewModelTest:BaseViewModelFixture
    {
        [Fact]
        public void ShouldBeUpdateUserWhenCallUpdateUserMethod()
        {
            var userService = new UserService(new UserServiceApiMock(), Mapper);
            var fixture = new EditProfileViewModelFixture()
                .UserServiceWith(userService);
            EditProfileViewModel editProfileViewModel = fixture;
            editProfileViewModel.InitializeCommand.Execute().Subscribe();
            var user = editProfileViewModel.User;
            user.Names = "test";
            var result =    editProfileViewModel.UpdateCommand.Execute();
            using var observedSequence = result.Observe();
            observedSequence.RecordedMessages.Should().Satisfy(x => x.Id == user.Id && user.Names == x.Names);
        }
    }
}