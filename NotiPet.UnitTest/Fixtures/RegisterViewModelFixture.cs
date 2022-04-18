using NotiPet.Domain.Service;
using NotiPet.Domain.Validator;
using NotiPet.UnitTest.ViewModelTest;
using NotiPetApp.Services;
using NotiPetApp.ViewModels;

namespace NotiPet.UnitTest.Fixtures
{
    public class RegisterViewModelFixture: BaseViewModelFixture,IBuilder
    {
    public static implicit operator RegisterViewModel(RegisterViewModelFixture registerViewModelFixture) =>
        registerViewModelFixture.Build();

    private RegisterViewModel Build() => new RegisterViewModel(NavigationService,PageDialogService,_authenticationService, new RegisterValidator(), new DeviceUtils());

    public RegisterViewModelFixture AuthenticationWith(IAuthenticationService authenticationService) =>
        this.With(ref _authenticationService, authenticationService);
    private IAuthenticationService _authenticationService;
    
    }
}