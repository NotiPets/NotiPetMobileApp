using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Bogus.DataSets;
using DynamicData.Binding;
using FluentValidation;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPet.Domain.Validator;
using NotiPetApp.Helpers;
using NotiPetApp.Properties;
using NotiPetApp.Services;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.States;

namespace NotiPetApp.ViewModels
{
    public class LoginViewModel:BaseViewModel,IAuthenticationRequestViewModel,IValidatableViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly AuthenticationValidator _validator;
        private readonly ISchedulerProvider _schedulerProvider;
        [Reactive]  public string Email { get; set; }
        

       [Reactive]    public string Password { get; set; }
       [Reactive]   public string Username { get; set; }
       public ReactiveCommand<Unit,Unit> ChangeAuthenticationCommand { get; set; }
        public ReactiveCommand<Unit,Unit> ForgotPasswordCommand { get; set; }
        public ReactiveCommand<Unit,Authentication> AuthenticationCommand { get; set; }
        public ReactiveCommand<Authentication,Unit> NavigateToMenuPageCommand { get; set; }
        public ReactiveCommand<Unit,Unit> NavigateToSignUpCommand{ get; set; }
        public ReactiveCommand<Unit,Unit> NavigateGoBackCommand { get; set; }
        protected ObservableAsPropertyHelper<string> _errorMessage;
       public string ErrorMessage => _errorMessage.Value;
        public LoginViewModel(INavigationService navigationService, IPageDialogService dialogPage,
            IAuthenticationService authenticationService, AuthenticationValidator validator,
            ISchedulerProvider schedulerProvider) : base(navigationService, dialogPage)
        {
            
            ValidationContext = new ValidationContext();
            _authenticationService = authenticationService;
            _validator = validator;
            _schedulerProvider = schedulerProvider;
            this.WhenAnyValue(e => e.Email)
                .StartWith(string.Empty)
                .ToProperty(this, x => x.Email)
                .DisposeWith(Subscriptions);
            ForgotPasswordCommand = ReactiveCommand.CreateFromTask(() =>
            {
                //TODO Navigate to Fotgot password;
                return  Task.CompletedTask;
            });
            NavigateToSignUpCommand = ReactiveCommand.CreateFromTask<Unit>((param) =>
            {
                return NavigationService.NavigateAsync(ConstantUri.Register);
            });
  
               
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b,token) =>   NavigationService.GoBackAsync());
            AuthenticationCommand = ReactiveCommand.CreateFromObservable(Authentication,ValidationContext.Valid);
            var canExecuteMenu = AuthenticationCommand.Select(e => e != null&&!string.IsNullOrEmpty(e.Jwt));
            NavigateToMenuPageCommand = ReactiveCommand.CreateFromTask<Authentication>((param) =>
            {
                Settings.SetToken(param.Jwt);
                Settings.Username = param.User.Username;
                Settings.UserId = param.User.Id;
                return NavigationService.NavigateAsync(ConstantUri.TabMenu);
            },canExecuteMenu);
            AuthenticationCommand.ThrownExceptions.Subscribe((x =>
            {
                RxApp.MainThreadScheduler.Schedule(() =>
                {
                    dialogPage.DisplayAlertAsync("Error",AppResources.ErrorLoginInformations,"Ok");
                    // throw ex;
                });

            })).DisposeWith(Subscriptions);
            AuthenticationCommand
                .InvokeCommand(NavigateToMenuPageCommand);
            ActiveValidation();
            ActiveMethod();
        }

        private void ActiveMethod()
        {
            if (_schedulerProvider==null)
            {
                Debug.WriteLine($"_schedulerProvider IS NULL");
                return;
            }
            _errorMessage = AuthenticationCommand
                .Skip(1)
                .Select(e=>e!=null&&!string.IsNullOrEmpty(e.Jwt)?string.Empty:AppResources.ErrorLoginInformations)
                .ToProperty(this,x=>x.ErrorMessage,scheduler:_schedulerProvider.MainThread);
            _isBusy = AuthenticationCommand
                .IsExecuting.ToProperty(this,x=>x.IsBusy,scheduler:_schedulerProvider.CurrentThread);
        }

        void ActiveValidation()
        {
          
            IObservable<IValidationState> passwordValidation = this.WhenAnyValue(e => e.Password)
                .StartWith()
                .Skip(1)
                .Select(_ => this)
                .Select(_validator.Validate)
                .Select(e=> !e.IsValid&&e.Errors.HasMessageForProperty(nameof(Password))? new ValidationState(false,e.Errors.GetMessageForProperty(nameof(Password))):ValidationState.Valid);
            IObservable<IValidationState> userNameValidation = this.WhenAnyValue(e => e.Username)
                .StartWith()
                .Skip(1)
                .Select(_ => this)
                .Select(_validator.Validate)
                .Select(e=> !e.IsValid&&e.Errors.HasMessageForProperty(nameof(Username))? new ValidationState(false,e.Errors.GetMessageForProperty(nameof(Username))):ValidationState.Valid);

            this.ValidationRule(e => e.Username, userNameValidation);
            this.ValidationRule(e => e.Password, passwordValidation);
            
        }

        IObservable<Authentication> Authentication()
        {
            var observable = _authenticationService.Authentication(this);
            return observable;
        }


        public ValidationContext ValidationContext { get; }

    }
}