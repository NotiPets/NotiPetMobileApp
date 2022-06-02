using System;
using System.Diagnostics;
using System.Reactive.Concurrency;
using Prism.Services;
using ReactiveUI;

namespace NotiPetApp
{
    public class RxExceptionHandler : IObserver<Exception>
    {
        private readonly IPageDialogService _dialogService;

        public RxExceptionHandler(IPageDialogService dialogService)
        {
            _dialogService = dialogService;
        }
        public void OnNext(Exception ex)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }

            RxApp.MainThreadScheduler.Schedule(() =>
            {
                _dialogService.DisplayAlertAsync("Error",ex.Message,"Ok");
                // throw ex;
            });
        }

        public void OnError(Exception ex)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }

            RxApp.MainThreadScheduler.Schedule(() =>
            {
                _dialogService.DisplayAlertAsync("Error",ex.Message,"Ok");
               // throw ex;
            });
        }

        public void OnCompleted()
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }

            RxApp.MainThreadScheduler.Schedule(() => { throw new NotImplementedException(); });
        }
    }
}