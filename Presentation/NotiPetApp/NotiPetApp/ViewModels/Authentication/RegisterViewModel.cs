using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPet.Domain.Validator;
using NotiPetApp.Helpers;
using NotiPetApp.Properties;
using NotiPetApp.Services;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.States;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace NotiPetApp.ViewModels
{
    public class RegisterViewModel : BaseViewModel,IRegisterRequestViewModel,IValidatableViewModel
    {
        private readonly RegisterValidator _validator;
        private readonly IDeviceUtils _deviceUtils;
        private readonly IAuthenticationService _authenticationService;
        private PersonalDocument _personalDocument;
        [Reactive]  public string Email { get; set; }
        

        [Reactive]    public string Password { get; set; }
        [Reactive]    public string ConfirmPassword { get; set; }

        [Reactive]   public string Username { get; set; }
        [Reactive]   public string Name { get; set; }
        [Reactive]   public string LastName { get; set; }
        [Reactive]   public string Phone { get; set; }
        [Reactive]  public string Address1 { get; set; }
        [Reactive]  public string Address2 { get; set; }
        [Reactive]   public string City { get; set; }
        [Reactive]   public string Province { get; set; }
        [Reactive]   public int DocumentType  { get; set; }
        [Reactive]   public string  Document { get; set; }
        [Reactive] public int BusinessId { get; set; } = 1;

        [Reactive]   public string PicturePhoto { get; set; }

        public List<PersonalDocument> DocumentTypes => _documentTypes?.Value;
       private ObservableAsPropertyHelper<List<PersonalDocument>> _documentTypes;
       [Reactive] public PersonalDocument PersonalDocument
        {
            get => _personalDocument;
            set
            {
                _personalDocument = value;
                if (_personalDocument!=null)
                {
                    DocumentType = _personalDocument.DocumentId;
                }   
            }
        }

        public ReactiveCommand<Unit,Unit> NavigateGoBackCommand { get; set; }
        public ReactiveCommand<Authentication,Unit> NavigateToTabMenuCommand { get; set; }
        public ReactiveCommand<Unit,Authentication> AuthenticationCommand { get; set; }


        public RegisterViewModel(INavigationService navigationService, IPageDialogService dialogPage,
            IAuthenticationService authenticationService,RegisterValidator validator,IDeviceUtils deviceUtils) : base(navigationService, dialogPage)
        {
            _authenticationService = authenticationService;
            _validator = validator;
            _deviceUtils = deviceUtils;
            ValidationContext = new ValidationContext();
           
            AuthenticationCommand = ReactiveCommand.CreateFromObservable<Authentication>(Register,ValidationContext.Valid);
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b,token) =>   NavigationService.GoBackAsync());
            GoToTermAndConditionsCommand = ReactiveCommand.CreateFromTask<Unit>((b,token) =>   NavigationService.NavigateAsync(ConstantUri.TermsAndConditionsPage));
            GetPhotoCommand = ReactiveCommand.CreateFromTask(TakePhoto);
            var canExecuteNavigate = AuthenticationCommand.Select(e => e != null&&!string.IsNullOrEmpty(e.Jwt));
            NavigateToTabMenuCommand = ReactiveCommand.CreateFromTask<Authentication>(((b, token) =>
            {
                Settings.SetToken(b.Jwt);
                Settings.Username = b.User.Username;
                Settings.UserId = b.User.Id;
                return NavigationService.NavigateAsync(ConstantUri.TabMenu);
            }),canExecuteNavigate);
            AuthenticationCommand
                .InvokeCommand(NavigateToTabMenuCommand);
           ActiveValidation();
        }

        public ReactiveCommand<Unit, Unit> GetPhotoCommand { get; set; }

        async Task TakePhoto()
        {
            PicturePhoto = await _deviceUtils.TakePhotoAsync();
        }
        protected override IObservable<Unit> ExecuteInitialize()
        {
            return Observable.Create<Unit>(observer =>
            {
                var compositeDisposable = new CompositeDisposable();
               var getDocuments =   _authenticationService.GetDocumentTypes();
               _documentTypes = getDocuments.ToProperty(this,x=>x.DocumentTypes);
               getDocuments
                     .Select(e=>Unit.Default)
                     .Subscribe(observer)
                     .DisposeWith(compositeDisposable);
                 return compositeDisposable;
            });
        }
        IObservable<Authentication> Register()
        {
            return _authenticationService.SignUp(this).Select(x=>x);
        }
        void ActiveValidation()
        {
            IObservable<IValidationState> emailValidation = this.WhenAnyValue(e => e.Email)
                .Skip(1)
                .Select(_ => this)
                .Select(_validator.Validate)
                .Select(e=> !e.IsValid&&e.Errors.HasMessageForProperty(nameof(Email))?  new ValidationState(false,e.Errors.GetMessageForProperty(nameof(Email))):ValidationState.Valid);

            IObservable<IValidationState>  nameValidation = this.WhenAnyValue(e => e.Name)
                .Select(_ => this)
                .Select(_validator.Validate)
                .Select(e=> !e.IsValid&&e.Errors.HasMessageForProperty(nameof(Name))?  new ValidationState(false,e.Errors.GetMessageForProperty(nameof(Name))):ValidationState.Valid);

            IObservable<IValidationState>  lastNameValidation = this.WhenAnyValue(e => e.LastName)
                .Skip(1)
                .Select(_ => this)
                .Select(_validator.Validate)
                .Select(e=> !e.IsValid&&e.Errors.HasMessageForProperty(nameof(LastName))?
                    new ValidationState(false,e.Errors.GetMessageForProperty(nameof(LastName))):ValidationState.Valid);
            IObservable<IValidationState>  personalDocument = this.WhenAnyValue(e => e.Document)
                .Skip(1).
                Select(_ => this)
                .Select(_validator.Validate)
                .Select(e=> !e.IsValid&&e.Errors.HasMessageForProperty(nameof(Document))?
                    new ValidationState(false,e.Errors.GetMessageForProperty(nameof(Document))):ValidationState.Valid);
            
            IObservable<IValidationState> passwordValidation = this.WhenAnyValue(e => e.Password)
                .StartWith()
                .Skip(1)
                .Select(_ => this)
                .Select(_validator.Validate)
                .Select(e=> !e.IsValid&&e.Errors.HasMessageForProperty(nameof(Password))? new ValidationState(false,e.Errors.GetMessageForProperty(nameof(Password))):ValidationState.Valid);
        
            IObservable<IValidationState> confirmPasswordValidation = this.WhenAnyValue(e => e.ConfirmPassword)
                .StartWith()
                .Skip(1)
                .Select(_ => this)
                .Select(_validator.Validate)
                .Select(e=> !e.IsValid&&e.Errors.HasMessageForProperty(nameof(ConfirmPassword))? new ValidationState(false,e.Errors.GetMessageForProperty(nameof(ConfirmPassword))):ValidationState.Valid);
            
            IObservable<IValidationState> userNameValidation = this.WhenAnyValue(e => e.Username)
                .StartWith()
                .Skip(1)
                .Select(_ => this)
                .Select(_validator.Validate)
                .Select(e=> !e.IsValid&&e.Errors.HasMessageForProperty(nameof(Username))? new ValidationState(false,e.Errors.GetMessageForProperty(nameof(Username))):ValidationState.Valid);
            
            IObservable<IValidationState> cityValidation = this.WhenAnyValue(e => e.City)
                .StartWith()
                .Skip(1)
                .Select(_ => this)
                .Select(_validator.Validate)
                .Select(e=> !e.IsValid&&e.Errors.HasMessageForProperty(nameof(City))? new ValidationState(false,e.Errors.GetMessageForProperty(nameof(City))):ValidationState.Valid);

            IObservable<IValidationState> provinceValidation = this.WhenAnyValue(e => e.Province)
                .StartWith()
                .Skip(1)
                .Select(_ => this)
                .Select(_validator.Validate)
                .Select(e=> !e.IsValid&&e.Errors.HasMessageForProperty(nameof(Province))? new ValidationState(false,e.Errors.GetMessageForProperty(nameof(Province))):ValidationState.Valid);

            
            IObservable<IValidationState> phoneValidation = this.WhenAnyValue(e => e.Phone)
                .StartWith()
                .Skip(1)
                .Select(_ => this)
                .Select(_validator.Validate)
                .Select(e=> !e.IsValid&&e.Errors.HasMessageForProperty(nameof(Phone))? new ValidationState(false,e.Errors.GetMessageForProperty(nameof(Phone))):ValidationState.Valid);

            this.ValidationRule(e => e.Username, userNameValidation);
            this.ValidationRule(e => e.Name, nameValidation);
            this.ValidationRule(e => e.LastName, lastNameValidation);
            this.ValidationRule(e => e.Email, emailValidation);
            this.ValidationRule(e => e.Password, passwordValidation);
            this.ValidationRule(e => e.ConfirmPassword,confirmPasswordValidation);
            this.ValidationRule(e => e.Document, personalDocument);
            this.ValidationRule(e => e.City, cityValidation);
            this.ValidationRule(e => e.Province, provinceValidation);
            this.ValidationRule(e => e.Phone, phoneValidation);
            
        }
        public ValidationContext ValidationContext { get; }

        public ReactiveCommand<Unit,Unit> GoToTermAndConditionsCommand { get; set; }
    }
}