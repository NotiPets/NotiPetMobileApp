using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Properties;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NotiPetApp.ViewModels
{
    public class ForgotPasswordViewModel:BaseViewModel
    {
        private readonly IAuthenticationService _authentication;
        [Reactive] public string Email { get; set; }
        [Reactive] public int Code { get; set; }
        [Reactive] public string Password { get; set; }
        [Reactive] public string ConfirmPassword { get; set; }
        public ReactiveCommand<Unit, User> ValidateCodeCommand { get; set; }
        public ReactiveCommand<Unit,bool> SendEmailCommand { get; set; }
        public ReactiveCommand<Unit, bool> UpdatePasswordCommand { get; set; }
        private ObservableAsPropertyHelper<User> _user;
        public User User => _user?.Value;
        [Reactive] public int Step { get; set; }
        public string CommandText { get; set; }
        public ForgotPasswordViewModel(INavigationService navigationService, IPageDialogService dialogPage,IAuthenticationService authentication) : base(navigationService, dialogPage)
        {
            _authentication = authentication;
            Step = 0;
            CommandText = AppResources.Continue;
            SendEmailCommand = ReactiveCommand.CreateFromObservable<bool>(SendEmail,this.WhenAnyValue(x => x.Email).Select(x => !string.IsNullOrEmpty(x)));
            SendEmailCommand
                .InvokeCommand(ReactiveCommand.CreateFromTask<bool>(ShowMessageMail));
            ValidateCodeCommand = ReactiveCommand.CreateFromObservable<User>( ValidateCode,this.WhenAnyValue(x => x.Code).Select(x => $"{x}"?.Length > 1));
            ValidateCodeCommand.ThrownExceptions.Subscribe(x =>
            {
                RxApp.MainThreadScheduler.Schedule(() =>
                {
                    DialogPage.DisplayAlertAsync("Error","Codigo de verificación incorrecto","Ok");
                    // throw ex;
                });
            });
            _user = ValidateCodeCommand.ToProperty(this, x => x.User);
            ValidateCodeCommand.Select(x => x != null).Subscribe(next =>
            {
                if (next)
                {
                    Step = 2;
                    CommandText = AppResources.Update;
                }
            });
            
            UpdatePasswordCommand = ReactiveCommand
                .CreateFromObservable<bool>( UpdatePassword,this.WhenAnyValue(x => x.Password).Select(x=>x==ConfirmPassword));
            UpdatePasswordCommand
                .Where(x => x)
                .Select(x=> Unit.Default)
                .InvokeCommand(ReactiveCommand.CreateFromTask(GoBack));
            ContinueCommand = ReactiveCommand.CreateFromObservable(Continue);
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());
            ValidateCodeCommand
                .DisposeWith(Subscriptions);
        }

        public ReactiveCommand<Unit, Unit> NavigateGoBackCommand { get; set; }

        public ReactiveCommand<Unit, Unit> ContinueCommand { get; set; }

        IObservable<Unit> Continue()
        {
            switch (Step)
            {
                case 0:
                  return  SendEmailCommand.Execute().Select(x=>Unit.Default);
                    break;
                case 1:
                    return  ValidateCodeCommand.Execute().Select(x=>Unit.Default);;
                    break;
                case 2:
                    return UpdatePasswordCommand.Execute().Select(x=>Unit.Default);;
                    break;
                    
            }

            return Observable.Empty<Unit>();
        }
        async  Task GoBack()
        {
            await DialogPage.DisplayAlertAsync(AppResources.Alert,
                "Contraseña cambiada correctamente.", "OK");
          await  NavigationService.GoBackAsync();
        }
        async Task ShowMessageMail(bool canContinue)
       {
           if (canContinue)
           {
               await DialogPage.DisplayAlertAsync(AppResources.Alert,
                   "Se ha enviado un código de verificación   a su correo electrónico, para poder continuar al cambio de contraseña.", "OK");
               Step = 1;
           }

       }
        private IObservable<bool> UpdatePassword()
        {
            return _authentication.UpdatePassword(User.Id,Password);
        }


        private IObservable<User> ValidateCode()
        {
            return _authentication.ValidateCode(Code);
        }

        IObservable<bool> SendEmail()
        {
            return _authentication.ForgotPassword(Email);
        }
    }
}