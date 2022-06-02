using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;

using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
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
        public ReactiveCommand<string,INavigationResult> LogInCommand { get; set; }

        public SocialNetworkAuthenticationViewModel(INavigationService navigationService, IPageDialogService dialogPage,IAuthenticationService authenticationService) : base(navigationService, dialogPage)
        {
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
            LogInCommand = ReactiveCommand.CreateFromObservable<string,INavigationResult>(Authentication);
        }

        public ReactiveCommand<Unit, Unit> NavigateToSignUpCommand { get; set; }

        IObservable<INavigationResult> Authentication(string code) => Observable.FromAsync(() =>
        {
           return code switch
            {
                "FB" => NavigationService.NavigateAsync(ConstantUri.TabMenu),
                "GB" => NavigationService.NavigateAsync(ConstantUri.TabMenu),
                "Skip" => NavigationService.NavigateAsync(ConstantUri.TabMenu),
                _ => NavigationService.NavigateAsync(ConstantUri.Login)
            };
        });
          

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