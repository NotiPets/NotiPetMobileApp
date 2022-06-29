using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NotiPetApp.ViewModels
{
    public class EditAppointmentViewModel:BaseViewModel,IInitialize
    {
        private readonly ISalesService _salesService;
        [Reactive] public AppointmentSale AppointmentSale { get; set; }
        public EditAppointmentViewModel(INavigationService navigationService, IPageDialogService dialogPage,ISalesService salesService ) : base(navigationService, dialogPage)
        {
            _salesService = salesService;
            var cancelAppointmentCommand = ReactiveCommand.CreateFromObservable<string,bool>(CancelAppointment);
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b,token) =>   NavigationService.GoBackAsync());
            CanCancelCommand = ReactiveCommand.CreateFromTask<string,string>(CanCancelOrder);
            CanCancelCommand
                .Where(e=>!string.IsNullOrEmpty(e))
                .InvokeCommand(cancelAppointmentCommand);
            cancelAppointmentCommand.Select(x=>Unit.Default).InvokeCommand(NavigateGoBackCommand);
            UpdateAppointmentCommand = ReactiveCommand.CreateFromObservable<Unit>(UpdateAppointment);
            UpdateAppointmentCommand
                .InvokeCommand(NavigateGoBackCommand);
        }

        public ReactiveCommand<Unit, Unit> UpdateAppointmentCommand { get; set; }


        public ReactiveCommand<Unit, System.Reactive.Unit> NavigateGoBackCommand { get; set; }

        IObservable<Unit> UpdateAppointment()
        {
            return _salesService.UpdateAppointment(AppointmentSale.Appointment)
                .Select(x=>Unit.Default);
        }
        IObservable<bool> CancelAppointment(string id)
        {
            return _salesService.CancelOrder(id);
        }
        public ReactiveCommand<string, string> CanCancelCommand { get; set; }

        [Reactive] public  AssetServiceModel SelectedService { get; set; }

        public async Task<string> CanCancelOrder(string id)
        {
            var can = await DialogPage.DisplayAlertAsync("Alert", "Are you sure want to cancel order?", "YES", "NO");
            return can ? id : string.Empty;
        }

        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(ParameterConstant.Appointment))
            {
                AppointmentSale = parameters[ParameterConstant.Appointment] as AppointmentSale; 
                
            }
        }
    }
}