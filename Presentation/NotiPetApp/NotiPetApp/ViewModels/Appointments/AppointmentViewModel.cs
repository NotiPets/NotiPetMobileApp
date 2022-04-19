using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
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
        private ReadOnlyObservableCollection<Appointment> _appointments;
        public ReadOnlyObservableCollection<Appointment> Appointments => _appointments;
        [Reactive]public int SelectedIndex { get; set; }
        public AppointmentViewModel(INavigationService navigationService, IPageDialogService dialogPage,ISalesService salesService) : base(navigationService, dialogPage)
        {
            _salesService = salesService;
            var filterPredicate = this.WhenAnyValue(x => x.SelectedIndex)
                .Throttle(TimeSpan.FromMilliseconds(100), RxApp.TaskpoolScheduler)
                .DistinctUntilChanged()
                .Select(FilterBy);
            salesService.AppointmentDatasource
                .Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Filter(filterPredicate)
                .Sort(SortExpressionComparer<Appointment>.Descending(e=>e.Date))
                .Bind(out _appointments)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b,token) =>   NavigationService.GoBackAsync());
            
        }

        Func<Appointment, bool> FilterBy(int index) => index switch
        {
            0 => new Func<Appointment, bool>(x => x.AppointmentStatus <= EAppointmentStatus.Accepted),
            _ => new Func<Appointment, bool>(x => x.AppointmentStatus >= EAppointmentStatus.Cancelled),
        };

        protected override IObservable<Unit> ExecuteInitialize()
            => _salesService.GetAppointmentByUserId(Settings.UserId)
                .Select(e=>Unit.Default);

        public ReactiveCommand<Unit,Unit> NavigateGoBackCommand { get; set; }
    }
}