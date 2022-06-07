using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ImTools;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace NotiPetApp.ViewModels
{
    public class EditAppointmentViewModel:BaseViewModel,IInitialize
    {
        private readonly ISalesService _salesService;
        public AppointmentSale AppointmentSale { get; set; }
        public EditAppointmentViewModel(INavigationService navigationService, IPageDialogService dialogPage,ISalesService salesService ) : base(navigationService, dialogPage)
        {
            _salesService = salesService;
            var cancelAppointmentCommand = ReactiveCommand.CreateFromObservable<string,bool>(CancelAppointment);
            CanCancelCommand = ReactiveCommand.CreateFromTask<string,string>(CanCancelOrder);
            CanCancelCommand
                .Where(e=>!string.IsNullOrEmpty(e))
                .InvokeCommand(cancelAppointmentCommand);

        }

        IObservable<AppointmentSale> UpdateAppointment()
        {
            throw new Exception();
        }
        IObservable<bool> CancelAppointment(string id)
        {
            return _salesService.CancelOrder(id);
        }
        public ReactiveCommand<string, string> CanCancelCommand { get; set; }

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