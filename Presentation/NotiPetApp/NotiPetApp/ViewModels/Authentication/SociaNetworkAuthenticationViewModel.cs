using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using Newtonsoft.Json;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using NotiPetApp.Properties;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace NotiPetApp.ViewModels
{
    public class SocialNetworkAuthenticationViewModel:BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private ReadOnlyObservableCollection<SocialNetwork> _socialNetworks;
        public ReadOnlyObservableCollection<SocialNetwork> SocialNetworks => _socialNetworks;
        public ReactiveCommand<Unit,Unit> InitializeCommand { get; set; }
        public ReactiveCommand<string,Unit> LogInCommand { get; set; }
        IGoogleClientManager _googleService;

        public SocialNetworkAuthenticationViewModel(INavigationService navigationService, IPageDialogService dialogPage, IGoogleClientManager googleService,IAuthenticationService authenticationService) : base(navigationService, dialogPage)
        {
            _googleService = googleService;
            _authenticationService = authenticationService;
            _authenticationService.SocialNetworks.Connect()
                .Bind(out _socialNetworks)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);
            InitializeCommand = ReactiveCommand.CreateFromObservable(InitializeData);
            NavigateToSignUpCommand = ReactiveCommand.CreateFromTask<Unit>((param) =>
            {
                return NavigationService.NavigateAsync(ConstantUri.Register);
            });
            LogInCommand = ReactiveCommand.CreateFromTask<string>(Authentication);
        }

        public ReactiveCommand<Unit, Unit> NavigateToSignUpCommand { get; set; }

        Task Authentication(string code)
        {
            return code switch
            {
                "FB" => NavigationService.NavigateAsync(ConstantUri.TabMenu),
                "GB" => LoginGoogleAsync(),
                "Skip" => NavigationService.NavigateAsync(ConstantUri.TabMenu),
                _ => NavigationService.NavigateAsync(ConstantUri.Login)
            };
        }
               async Task LoginGoogleAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(_googleService.AccessToken))
                {
                    //Always require user authentication
                    _googleService.Logout();
                }
           
                try
                {
                    _googleService.OnLogin += OnLoginCompleted;
                    _googleService.OnError += (sender, args) =>
                    {
                        Console.WriteLine(args.Error);
                    };
                    Console.WriteLine(_googleService.IdToken);
                    await _googleService.LoginAsync();
                }
                catch (GoogleClientSignInCanceledErrorException ex)
                {
                    Console.WriteLine(ex);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }
               private void OnLoginCompleted(object sender, GoogleClientResultEventArgs<GoogleUser> loginEventArgs)
               {
                   if (loginEventArgs.Data != null)
                   {
                       GoogleUser googleUser = loginEventArgs.Data;


                   }
                   else
                   {
                       App.Current.MainPage.DisplayAlert("Error", loginEventArgs.Message, "OK");
                   }

                   _googleService.OnLogin -= OnLoginCompleted;

               }

        private IObservable<Unit> InitializeData()
        
        =>    Observable.Create<Unit>((value) =>
            {
                var disposable = new CompositeDisposable();
                _authenticationService.GetSocialNetworks()
                    .Select(e => Unit.Default)
                    .Subscribe()
                    .DisposeWith(Subscriptions);
                return disposable;
            });
        
    }
}