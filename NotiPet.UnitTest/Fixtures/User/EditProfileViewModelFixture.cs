using NotiPet.Domain.Service;
using NotiPet.UnitTest.ViewModelTest;
using NotiPetApp.ViewModels;

namespace NotiPet.UnitTest.Fixtures.User
{
    public class EditProfileViewModelFixture:BaseViewModelFixture,IBuilder
    {
        public static implicit operator EditProfileViewModel(EditProfileViewModelFixture fixture) => fixture.Build();

        private EditProfileViewModel Build() => new EditProfileViewModel(NavigationService,PageDialogService,_userService);

        public EditProfileViewModelFixture UserServiceWith(IUserService userService) =>
            this.With(ref _userService, userService);
        private IUserService _userService;
    }
}