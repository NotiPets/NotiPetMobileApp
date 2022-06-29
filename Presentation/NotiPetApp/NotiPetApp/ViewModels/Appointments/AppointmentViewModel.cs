using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
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
    public class AppointmentViewModel:BaseViewModel,INavigationAware
    {
        private readonly ISalesService _salesService;
        private ReadOnlyObservableCollection<AppointmentSale> _appointments;
        public ReadOnlyObservableCollection<AppointmentSale> Appointments => _appointments;
        
        
        [Reactive]public int SelectedIndex { get; set; }

        public ReactiveCommand<AppointmentSale,Unit> EditAppointmentCommand { get; set; }
        public AppointmentViewModel(INavigationService navigationService, IPageDialogService dialogPage,ISalesService salesService) : base(navigationService, dialogPage)
        {
            _salesService = salesService;
            var filterPredicate = this.WhenAnyValue(x => x.SelectedIndex)
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
            var cancelAppointmentCommand = ReactiveCommand.CreateFromObservable<string,bool>(CancelAppointment);
            CanCancelCommand = ReactiveCommand.CreateFromTask<string,string>(CanCancelOrder);
            EditAppointmentCommand = ReactiveCommand.CreateFromTask<AppointmentSale>(EditAppointment);
            CanCancelCommand
                .Where(e=>!string.IsNullOrEmpty(e))
                .InvokeCommand(cancelAppointmentCommand);
            cancelAppointmentCommand
                .Select(e=>Unit.Default)
                .InvokeCommand(InitializeCommand);
    
        }

        async Task EditAppointment(AppointmentSale appointmentSale)
        {
            if (appointmentSale.Appointment.CantEdit)
            {
                await NavigationService.NavigateAsync(ConstantUri.EditAppointment,
                    new NavigationParameters() {{ParameterConstant.Appointment, appointmentSale}}, true);

            }
        }

        IObservable<bool> CancelAppointment(string id)
        {
            return _salesService.CancelOrder(id);
        }
        public ReactiveCommand<string,string> CanCancelCommand { get; set; }

        public async Task<string> CanCancelOrder(string id)
        {
             var can = await DialogPage.DisplayAlertAsync("Alert", "Are you sure want to cancel order?", "YES", "NO");
             return can ? id : string.Empty;
        }

        Func<AppointmentSale, bool> FilterBy(int index) => index switch
        {
            0 => new Func<AppointmentSale, bool>(x => x.Appointment.AppointmentStatus <= EAppointmentStatus.Accepted),
            _ => new Func<AppointmentSale, bool>(x => x.Appointment.AppointmentStatus >= EAppointmentStatus.Cancelled),
        };

        protected override IObservable<Unit> ExecuteInitialize()
        {
            _salesService.ReceiveMessage(GetMessage);
            _salesService.Connect();
            return _salesService.GetSaleByUserId(Settings.UserId)
                .Select(e => Unit.Default);
        }
        

        private async  void GetMessage(string message)
        {
            if (message == "Muerte a Xamarin! atte. Amel")
            {
                await _salesService.GetSaleByUserId(Settings.UserId)
                    .Select(e => Unit.Default);
            }   

        }
        public ReactiveCommand<Unit,Unit> NavigateGoBackCommand { get; set; }
        public void Initialize(INavigationParameters parameters)
        {

        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() == NavigationMode.Back)
            {
                InitializeCommand
                    .Execute()
                    .Subscribe()
                    .DisposeWith(Subscriptions);
            }
        }
    }
}