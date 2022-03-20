using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

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
        public ReactiveCommand<Unit,Unit> InitializeCommand { get;  }
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

        public void Dispose()
        {
            Dispose(true);
        }
    }
}