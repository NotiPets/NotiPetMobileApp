﻿using System;
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
        private readonly AsyncSubject<Unit> _appearing = new AsyncSubject<Unit>();

        /// <summary>
        /// Gets an observable sequence indicating when a page is appearing.
        /// </summary>
        protected IObservable<Unit> WhenAppearing => _appearing.AsObservable();
        public BasePage()
        {
            On<iOS>().SetUseSafeArea(true);
            this.WhenActivated((disposable) => ManageDisposables(disposable));
        }
        
        protected override void OnAppearing()
        {
            _appearing.OnNext(Unit.Default);
            _appearing.OnCompleted();
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
                _appearing.Dispose();
                PageDisposables.Dispose();
            }
        }

    }
}