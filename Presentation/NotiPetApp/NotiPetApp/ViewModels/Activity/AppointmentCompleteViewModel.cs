using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace NotiPetApp.ViewModels.Activity
{
    public class AppointmentCompleteViewModel:BaseViewModel,IInitialize
    {
        private readonly IVeterinaryService _veterinaryService;
        public Sales Order { get; set; }
        public Veterinary Veterinary { get; set; }
        public DateTime Date { get; set; }

        public ReactiveCommand<Unit, Unit> GoToAppointments { get; }

        public AppointmentCompleteViewModel(INavigationService navigationService, 
            IPageDialogService dialogPage,IVeterinaryService veterinaryService) : base(navigationService, dialogPage)
        {
            _veterinaryService = veterinaryService;
            GoToAppointments = ReactiveCommand.CreateFromTask(async () =>
            {
               await NavigationService.NavigateAsync($"{ConstantUri.TabMenu}");
               await  NavigationService.NavigateAsync($"{ConstantUri.Appointment}");
            });
            GoToHomeCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                await NavigationService.NavigateAsync($"{ConstantUri.TabMenu}");
            });
        }

        public ReactiveCommand<Unit, Unit> GoToHomeCommand { get; set; }

        protected override IObservable<Unit> ExecuteInitialize()
        {
            return Observable.Create<Unit>((observer) =>
            {
                var disposable = new CompositeDisposable();
                _veterinaryService
                    .GetVeterinary(Order.BusinessId)
                    .BindTo(this, x => x.Veterinary)
                    .DisposeWith(disposable);
                return disposable;
            });

       
        }

        public void Initialize(INavigationParameters parameters)
        {
            Order = parameters[ParameterConstant.OrderComplete] as Sales;
            if (Order != null&&Order.Orders.Any())
                Date = Order.Orders.FirstOrDefault()!.Appointment.Date;
        }
    }
}