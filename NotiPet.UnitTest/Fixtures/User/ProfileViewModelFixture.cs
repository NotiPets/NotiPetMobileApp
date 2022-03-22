using NotiPet.Domain.Service;
using NotiPet.UnitTest.ViewModelTest;
using NotiPetApp.ViewModels;

namespace NotiPet.UnitTest.Fixtures.User
{
    public class ProfileViewModelFixture:BaseViewModelFixture,IBuilder
    {
        public static implicit operator ProfileViewModel(ProfileViewModelFixture profileViewModelFixture) =>
            profileViewModelFixture.Build();

        private ProfileViewModel Build() => new ProfileViewModel(NavigationService,PageDialogService,_userService);
        public ProfileViewModelFixture UserServiceWith(IUserService userService) =>
            this.With(ref _userService, userService);
        private IUserService _userService;
    }
}