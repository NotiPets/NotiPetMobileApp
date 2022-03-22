using System;
using FluentAssertions;
using NotiPet.Data.Services;
using NotiPet.Mocks.Services;
using NotiPet.UnitTest.Fixtures.User;
using NotiPet.UnitTest.Services;
using NotiPetApp.ViewModels;
using Xunit;

namespace NotiPet.UnitTest.ViewModelTest.UserTest
{
    public class ProfileViewModelTest:BaseViewModelFixture

    {
        [Fact]
        public void ShouldBeReturnUserWhenCallGetUserById()
        {
            var viewModel = new ProfileViewModelFixture()
                .UserServiceWith(new UserService(new UserServiceApiMock(),Mapper));
            ProfileViewModel profileViewModel = viewModel;
            profileViewModel
                .InitializeCommand
                .Execute()
                .Subscribe();
            profileViewModel.User.Should().NotBeNull();
        }
    }
}