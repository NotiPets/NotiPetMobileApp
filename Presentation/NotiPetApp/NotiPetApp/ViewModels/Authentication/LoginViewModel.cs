using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NotiPetApp.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;

      [Reactive]  public string Email { get; set; }
        

      [Reactive]    public string Password { get; set; }
        public bool IsRegister { get; set; }
        public ReactiveCommand<Unit,Unit> ChangeAuthenticationCommand { get; set; }
        public ReactiveCommand<Unit,Unit> ForgotPasswordCommand { get; set; }
        public ReactiveCommand<Unit,bool> AuthenticationCommand { get; set; }
        public ReactiveCommand<bool,Unit> NavigateToMenuPageCommand { get; set; }
        public LoginViewModel(INavigationService navigationService, IPageDialogService dialogPage,IAuthenticationService authenticationService) : base(navigationService, dialogPage)
        {
            _authenticationService = authenticationService;
            this.WhenAnyValue(e => e.Email)
                .StartWith(string.Empty)
                .ToProperty(this, x => x.Email)
                .DisposeWith(Subscriptions);
            ChangeAuthenticationCommand = ReactiveCommand.Create(() =>
            {
                IsRegister = !IsRegister;
            });
            ForgotPasswordCommand = ReactiveCommand.CreateFromTask(() =>
            {
                //TODO Navigate to Fotgot password;
                return  Task.CompletedTask;
            });
            NavigateToMenuPageCommand = ReactiveCommand.CreateFromTask<bool>(((b, token) =>
            {
                Settings.Username = Email;
                Settings.Password = Password;
                return NavigationService.NavigateAsync(ConstantUri.TabMenu);
            }));
            AuthenticationCommand = ReactiveCommand.CreateFromObservable(Authentication);
                                    AuthenticationCommand.InvokeCommand(NavigateToMenuPageCommand);

        }

        IObservable<bool> Authentication()
        {
             return IsRegister? _authenticationService.SignUp(null)
                 .Select(e=>e==null):_authenticationService.Authentication(Email, Password)
                    .Select(e=>e==null);
        }
    }
}