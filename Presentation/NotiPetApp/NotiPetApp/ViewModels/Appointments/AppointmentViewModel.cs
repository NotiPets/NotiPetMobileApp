using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using DynamicData.PLinq;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NotiPetApp.ViewModels
{
    public class AppointmentViewModel:BaseViewModel
    {
        private readonly ISalesService _salesService;
        private ReadOnlyObservableCollection<AppointmentSale> _appointments;
        public ReadOnlyObservableCollection<AppointmentSale> Appointments => _appointments;
        [Reactive]public int SelectedIndex { get; set; }
        public AppointmentViewModel(INavigationService navigationService, IPageDialogService dialogPage,ISalesService salesService) : base(navigationService, dialogPage)
        {
            _salesService = salesService;
            var filterPredicate = this.WhenAnyValue(x => x.SelectedIndex)
                .Throttle(TimeSpan.FromMilliseconds(100), RxApp.TaskpoolScheduler)
                .DistinctUntilChanged()
                .Select(FilterBy);
            salesService.DataSource
                .Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Filter(x=>x.Appointment!=null)
                .Filter(filterPredicate)
                .Sort(SortExpressionComparer<AppointmentSale>.Descending(e=>e.Appointment.Date))
                .Bind(out _appointments)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b,token) =>   NavigationService.GoBackAsync());
            
        }

        Func<AppointmentSale, bool> FilterBy(int index) => index switch
        {
            0 => new Func<AppointmentSale, bool>(x => x.Appointment.AppointmentStatus <= EAppointmentStatus.Accepted),
            _ => new Func<AppointmentSale, bool>(x => x.Appointment.AppointmentStatus >= EAppointmentStatus.Cancelled),
        };

        protected override IObservable<Unit> ExecuteInitialize()
            => _salesService.GetSaleByUserId(Settings.UserId)
                .Select(e=>Unit.Default);

        public ReactiveCommand<Unit,Unit> NavigateGoBackCommand { get; set; }
    }
}