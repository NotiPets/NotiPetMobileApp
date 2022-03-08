using FluentValidation;
using Microsoft.Reactive.Testing;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPet.Domain.Validator;
using NotiPet.UnitTest.ViewModelTest;
using NotiPetApp.Services;
using NotiPetApp.ViewModels;

namespace NotiPet.UnitTest.Fixtures
{
    public class LoginViewModelFixture:BaseViewModelFixture,IBuilder
    {
        public static implicit operator LoginViewModel(LoginViewModelFixture loginViewModelFixture) =>
            loginViewModelFixture.Build();

        private LoginViewModel Build() => new LoginViewModel(NavigationService,PageDialogService,_authenticationService, new AuthenticationValidator(),new RegisterValidator(),_schedulerProvider);

        public LoginViewModelFixture AuthenticationWith(IAuthenticationService authenticationService) =>
            this.With(ref _authenticationService, authenticationService);
        private IAuthenticationService _authenticationService;

        public LoginViewModelFixture SchedulerProviderWith(SchedulerProvider schedulerProvider) =>
            this.With(ref _schedulerProvider, schedulerProvider);



        private ISchedulerProvider _schedulerProvider;
    }
}