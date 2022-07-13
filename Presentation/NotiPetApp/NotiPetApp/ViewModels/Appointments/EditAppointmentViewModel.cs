using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPet.Domain.Validator;
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
        private readonly CreateAppointmentValidate _validator;
        [Reactive] public AppointmentSale AppointmentSale { get; set; }
        [Reactive]  public DateTime Date { get; set; }
        [Reactive]  public TimeSpan TimeOfDay { get; set; }
        public DateTime MDateTime =>DateTime.Now;
        public EditAppointmentViewModel(INavigationService navigationService, IPageDialogService dialogPage,ISalesService salesService,CreateAppointmentValidate validator) : base(navigationService, dialogPage)
        {
            _salesService = salesService;
            _validator = validator;
            var cancelAppointmentCommand = ReactiveCommand.CreateFromObservable<string,bool>(CancelAppointment);
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b,token) =>   NavigationService.GoBackAsync());
            CanCancelCommand = ReactiveCommand.CreateFromTask<string,string>(CanCancelOrder);
            CanCancelCommand
                .Where(e=>!string.IsNullOrEmpty(e))
                .InvokeCommand(cancelAppointmentCommand);
            cancelAppointmentCommand.Select(x=>Unit.Default).InvokeCommand(NavigateGoBackCommand);
            UpdateAppointmentCommand = ReactiveCommand.CreateFromObservable<bool>(UpdateAppointment);
            UpdateAppointmentCommand
                .Select(x=>x)
                .Select(x=>Unit.Default)
                .InvokeCommand(NavigateGoBackCommand);
        }

        public ReactiveCommand<Unit, bool> UpdateAppointmentCommand { get; set; }


        public ReactiveCommand<Unit, System.Reactive.Unit> NavigateGoBackCommand { get; set; }

        IObservable<bool> UpdateAppointment()
        {
            Date = Date.Date.Add(TimeOfDay);
            var validate = _validator.Validate(new CreateAppointment(Date,AppointmentSale.Appointment.Id,Settings.UserId)
            {
                Veterinary = AppointmentSale.Veterinary,
                AssetServiceId = AppointmentSale.AssetService.Id,
                BusinessId = AppointmentSale.Veterinary.Id,
                AppointmentStatusId = (int)AppointmentSale.Appointment.AppointmentStatus
            });
            if (!validate.IsValid)
            {   
                ShowErrorMessage(validate.Errors.Select(e => e.ErrorMessage));
                return Observable.Return<bool>(false);
            }

            AppointmentSale.Appointment.Date = Date.Date.Add(TimeOfDay);
             
            return _salesService.UpdateAppointment(AppointmentSale.Appointment)
                .Select(x=>true);
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
                Date = AppointmentSale.Appointment.Date;
                TimeOfDay = AppointmentSale.Appointment.Date.TimeOfDay;
            }
        }
    }
}