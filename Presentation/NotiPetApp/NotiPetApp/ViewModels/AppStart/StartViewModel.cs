using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NotiPetApp.Helpers;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NotiPetApp.ViewModels
{
    public class StartViewModel:BaseViewModel
    {
     [Reactive]   public bool IsAnimating { get; set; }
        public ReactiveCommand<Unit,string> InitializeCommand { get; set; }
        public StartViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
            IsAnimating = true;
            InitializeCommand = ReactiveCommand.CreateFromObservable(()=>Settings.Token.Select(e => e));
            InitializeCommand.
                InvokeCommand(ReactiveCommand.CreateFromTask<string>(Initialize));
            this.WhenAnyValue(x => x.IsAnimating)
                .StartWith(true)
                .Where(e => !e)
                .Select(x => Unit.Default)
                .InvokeCommand(InitializeCommand)
                .DisposeWith(Subscriptions);
            
        }
         Task Initialize(string token)
         {
             return string.IsNullOrEmpty(token)
                 ? NavigationService.NavigateAsync(ConstantUri.SocialNetworkAuthentication)
                 : NavigationService.NavigateAsync(ConstantUri.TabMenu);
         }
    }
}