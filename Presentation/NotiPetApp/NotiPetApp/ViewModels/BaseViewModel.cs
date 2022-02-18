using System;
using System.Reactive.Disposables;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace NotiPetApp.ViewModels
{
    public abstract class BaseViewModel:ReactiveObject,IDestructible,IDisposable
    {
        protected CompositeDisposable Subscriptions { get; } = new CompositeDisposable();
        protected INavigationService NavigationService { get; }
        protected IPageDialogService DialogPage { get; }

        public BaseViewModel(INavigationService navigationService, IPageDialogService dialogPage)
        {
            NavigationService = navigationService;
            DialogPage = dialogPage;
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