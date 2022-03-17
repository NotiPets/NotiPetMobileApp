using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Bogus.DataSets;
using DynamicData.Binding;
using FluentValidation;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPet.Domain.Validator;
using NotiPetApp.Helpers;
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
    public class LoginViewModel:BaseViewModel,IAuthenticationRequestViewModel,IValidatableViewModel,IRegisterRequestViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly AuthenticationValidator _validator;
        private readonly RegisterValidator _registerValidator;
        private readonly ISchedulerProvider _schedulerProvider;
        [Reactive]  public string Email { get; set; }
        

       [Reactive]    public string Password { get; set; }
       [Reactive]   public string Username { get; set; }
       [Reactive]   public string Name { get; set; }
       [Reactive]   public string LastName { get; set; }
       [Reactive]   public string Phone { get; set; }
       [Reactive]  public string Address1 { get; set; }
       [Reactive]  public string Address2 { get; set; }
       [Reactive]   public string City { get; set; }
       [Reactive]   public string Province { get; set; }
       [Reactive]  public PersonalDocument PersonalDocument { get; set; }
       [Reactive] public string BusinessId { get; set; }
       public bool IsRegister { get; set; }
        public ReactiveCommand<Unit,Unit> ChangeAuthenticationCommand { get; set; }
        

        public ReactiveCommand<Unit,Unit> ForgotPasswordCommand { get; set; }
        public ReactiveCommand<Unit,string> AuthenticationCommand { get; set; }
        public ReactiveCommand<string,Unit> NavigateToMenuPageCommand { get; set; }
        
        public ReactiveCommand<string,Unit> NavigateToRegisterCommand { get; set; }

        public ReactiveCommand<Unit,Unit> NavigateGoBackCommand { get; set; }
        protected ObservableAsPropertyHelper<string> _errorMessage;
       public string ErrorMessage => _errorMessage.Value;
        public LoginViewModel(INavigationService navigationService, IPageDialogService dialogPage,
            IAuthenticationService authenticationService, AuthenticationValidator validator, RegisterValidator registerValidator,
            ISchedulerProvider schedulerProvider) : base(navigationService, dialogPage)
        {
            
            ValidationContext = new ValidationContext();
            _authenticationService = authenticationService;
            _validator = validator;
            _registerValidator = registerValidator;
            _schedulerProvider = schedulerProvider;
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
            NavigateToMenuPageCommand = ReactiveCommand.CreateFromTask<string>(((b, token) =>
            {
                if (string.IsNullOrEmpty(b)) 
                    return Task.CompletedTask;
                Settings.Token = Observable.Return(b);
                return NavigationService.NavigateAsync(ConstantUri.TabMenu);
            }));
            NavigateToRegisterCommand = ReactiveCommand.CreateFromTask<string>(((b, token) =>
            {
                  Settings.Token = Observable.Return(b);
                  return NavigationService.NavigateAsync(ConstantUri.Register);
             }));
               
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b,token) =>   NavigationService.GoBackAsync());
            AuthenticationCommand = ReactiveCommand.CreateFromObservable(Authentication,ValidationContext.Valid);
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
                .Select(e=>!string.IsNullOrEmpty(e)?string.Empty:"some of your info isn't correct, try again")
                .ToProperty(this,x=>x.ErrorMessage,scheduler:_schedulerProvider.CurrentThread);
            _isBusy = AuthenticationCommand
                .IsExecuting.ToProperty(this,x=>x.IsBusy,scheduler:_schedulerProvider.CurrentThread);
        }

        void ActiveValidation()
        {
            IObservable<IValidationState> emailValidation = this.WhenAnyValue(e => e.Email)
                .Select(_ => this)
                .Select(_registerValidator.Validate)
                .Select(e=> IsRegister&&!e.IsValid&&e.Errors.HasMessageForProperty(nameof(Email))?  new ValidationState(false,e.Errors.GetMessageForProperty(nameof(Email))):ValidationState.Valid);

            IObservable<IValidationState>  nameValidation = this.WhenAnyValue(e => e.Name)
                .Select(_ => this)
                .Select(_registerValidator.Validate)
                .Select(e=> IsRegister&&!e.IsValid&&e.Errors.HasMessageForProperty(nameof(Name))?  new ValidationState(false,e.Errors.GetMessageForProperty(nameof(Name))):ValidationState.Valid);

            IObservable<IValidationState>  lastNameValidation = this.WhenAnyValue(e => e.LastName)
                .Select(_ => this)
                .Select(_registerValidator.Validate)
                .Select(e=> IsRegister&&!e.IsValid&&e.Errors.HasMessageForProperty(nameof(LastName))?
                    new ValidationState(false,e.Errors.GetMessageForProperty(nameof(LastName))):ValidationState.Valid);
            IObservable<IValidationState>  personalDocument = this.WhenAnyValue(e => e.PersonalDocument)
                .Select(_ => this)
                .Select(_registerValidator.Validate)
                .Select(e=> IsRegister&&!e.IsValid&&e.Errors.HasMessageForProperty(nameof(PersonalDocument))?
                    new ValidationState(false,e.Errors.GetMessageForProperty(nameof(PersonalDocument))):ValidationState.Valid);
            
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
            this.ValidationRule(e => e.Name, nameValidation);
            this.ValidationRule(e => e.LastName, lastNameValidation);
            this.ValidationRule(e => e.Email, emailValidation);
            this.ValidationRule(e => e.Password, passwordValidation);
            
        }

        IObservable<string> Authentication()
        {
            var observable = IsRegister
                ? _authenticationService.SignUp(this)
                : _authenticationService.Authentication(this);
            return observable;

        }


        public ValidationContext ValidationContext { get; }

    }
}