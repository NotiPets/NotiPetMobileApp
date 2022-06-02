using System;
using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.XamForms;

namespace NotiPetApp.Views
{
    public class ContentViewBase<TViewModel> : ReactiveContentView<TViewModel>, IDisposable
        where TViewModel : ReactiveObject
    {
        /// <summary>
        /// Gets the view disposable.
        /// </summary>
        protected CompositeDisposable ViewDisposables { get; } = new CompositeDisposable();

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of the resources.
        /// </summary>
        /// <param name="disposing">A value indicating whether the instance is disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ViewDisposables.Dispose();
            }
        }
    }
}