using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Realms.Logging;

namespace NotiPetApp.ViewModels
{
    public abstract class BaseViewModel:ReactiveObject,IDestructible,IDisposable
    {
        protected CompositeDisposable Subscriptions { get; } = new CompositeDisposable();
        protected INavigationService NavigationService { get; }
        protected IPageDialogService DialogPage { get; }
        public bool IsBusy => _isBusy.Value;
        protected ObservableAsPropertyHelper<bool> _isBusy;
        protected virtual IObservable<Unit> ExecuteInitialize() => Observable.Empty<Unit>();
        public ReactiveCommand<Unit,Unit> InitializeCommand { get; protected set; }
        public BaseViewModel(INavigationService navigationService, IPageDialogService dialogPage)
        {
            NavigationService = navigationService;
            DialogPage = dialogPage;
            InitializeCommand = ReactiveCommand.CreateFromObservable(ExecuteInitialize);
        }
        public void Destroy()=> Subscriptions?.Dispose();
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Subscriptions.Dispose();
            }
        }
        public async Task ShowErrorMessage(IEnumerable<string> messages, bool canContinue = false)
        {
            var message = string.Join("\n", messages);
            await DialogPage.DisplayAlertAsync("Error", message,
                "Ok");

        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}