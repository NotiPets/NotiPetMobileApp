using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using NotiPetApp.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace NotiPetApp.Views
{
    public class BasePage<TViewModel>: ReactiveContentPage<TViewModel>,IDisposable
    where TViewModel:BaseViewModel
    {
        public BasePage()
        {
            On<iOS>().SetUseSafeArea(true);
            this.WhenActivated((disposable) => ManageDisposables(disposable));
        }

        protected virtual CompositeDisposable ManageDisposables(CompositeDisposable disposables)
        {
            return disposables;
        }
        protected CompositeDisposable PageDisposables { get; } = new CompositeDisposable();

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                PageDisposables.Dispose();
            }
        }

    }
}