using System;
using System.Diagnostics;
using System.Reactive;
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
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace NotiPetApp.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly AuthenticationValidator _validator;
        private readonly RegisterValidator _registerValidator;
        private readonly ISchedulerProvider _schedulerProvider;
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
        [Reactive]  public PersonalDocument PersonalDocument { get; set; }
        
        public ICommand StepBackCommand { get; set; }

        [Reactive] public string BusinessId { get; set; }
        public bool IsRegister { get; set; }
        public ReactiveCommand<Unit,Unit> NavigateGoBackCommand { get; set; }
        public ReactiveCommand<string,Unit> NavigateToTabMenuCommand { get; set; }
        public IAsyncCommand AuthenticationCommand { get; set; }

        public int Step { get; set; }
        

        public bool ValidatorPersonalInformation { get; set; }
        public bool ValidatorAddressInformation { get; set; }
        public bool ValidatorAccount { get; set; }

        private string skipButtonText;
        public string SkipButtonText { get; set; }

        private bool _showButton;

        public bool ShowButton
        {
            get=>!IsBusy && _showButton;
            set => _showButton = value;
        }


        public RegisterViewModel(INavigationService navigationService, IPageDialogService dialogPage,AuthenticationValidator validator) : base(navigationService, dialogPage)
        {
            _validator = validator;
            AuthenticationCommand = new AsyncCommand(ProcessStep);
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b,token) =>   NavigationService.GoBackAsync());
            SkipButtonText = "Next";
            NavigateToTabMenuCommand = ReactiveCommand.CreateFromTask<string>(((b, token) =>
            {
                Settings.Token = Observable.Return(b);
                return NavigationService.NavigateAsync(ConstantUri.TabMenu);
            }));
            StepBackCommand = new Command(() =>
            {
                Step -= 1;
                ShowButton = Step<2;
                SkipButtonText = "Next";

            });
        }
        
        async Task ProcessStep()
        {

            switch (Step)
            {
                case 0:
                    await AutoConfigMethod();
                    break;
                case 1:
                    await AuthenticationMobileUser();
                    break;
                case 2:
                    await  NavigateToMenu();
                    break;
            }
        }
        
        async Task AutoConfigMethod()
        {
            try
            {
                Step = -1;
                ShowButton = false;
                ValidatorPersonalInformation = !string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(LastName) ||
                                               !string.IsNullOrEmpty(PersonalDocument.ToString()) || !string.IsNullOrEmpty(Phone);
                if (ValidatorPersonalInformation)
                {
                        Step = 1;
                        ShowButton = true;
                        SkipButtonText = "Next";
                        return;
                }
                Step = 0;
                ShowButton = false;
            }
            catch (Exception e)
            {
                Step = 0;
                ShowButton = true;
            }
        }
        
        async Task AuthenticationMobileUser()
       {
           try
           {
               ShowButton = false;
               Step = -1;
               ValidatorPersonalInformation = !string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(LastName) ||
                                             !string.IsNullOrEmpty(PersonalDocument.ToString()) || !string.IsNullOrEmpty(Phone);
               ValidatorAddressInformation = !string.IsNullOrEmpty(Address1) || !string.IsNullOrEmpty(Address2) ||
                                             !string.IsNullOrEmpty(City) || !string.IsNullOrEmpty(Province);
               if (ValidatorPersonalInformation)
               {
                       Step = 1;
                       ShowButton = false;
                       if (ValidatorAddressInformation)
                       {
                           ShowButton = false;
                           Step = 2;
                           SkipButtonText = "Sign Up";
                           return;
                       }
                       return;
               }
                   await  NavigateToMenu();

           }
           catch (Exception e)
           {
               ShowButton = true;
               Step = 1;
           }
           finally
           {
               ShowButton = true;

           }
       }
        
        async  Task NavigateToMenu()
        {
            ValidatorAccount = !string.IsNullOrEmpty(Email) || !string.IsNullOrEmpty(Password) || !string.IsNullOrEmpty(ConfirmPassword);
            if (ValidatorAccount)
            {
                await NavigationService.NavigateAsync(ConstantUri.TabMenu);            
            }
        }

      

    }
}