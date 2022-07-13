using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using FluentValidation;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPet.Domain.Validator;
using NotiPetApp.Helpers;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NotiPetApp.ViewModels.Activity
{
    public class ConfirmAppointmentViewModel:BaseViewModel,IInitialize
    {
        private readonly ISalesService _salesService;
        private readonly IVeterinaryService _veterinaryService;
        private readonly IStoreService _storeService;
        private readonly CreateAppointmentValidate _validator;
        private CreateAppointment _createAppointment;
        public ReactiveCommand<Unit,Sales> CreateAppointmentCommand  { get; set; }
        [Reactive]public DateTime Date { get; set; }
        [Reactive]public TimeSpan Time { get; set; }

        public ReactiveCommand<Sales,Unit> RequestAppointmentCommand { get; set; }
        public Veterinary Veterinary { get; set; }
        public IEnumerable<AssetServiceModel> ServicesAvailable => _servicesAvailable?.Value;
        private ObservableAsPropertyHelper<IEnumerable<AssetServiceModel>> _servicesAvailable;
       [Reactive] public  AssetServiceModel SelectedService { get; set; }

        public ConfirmAppointmentViewModel(INavigationService navigationService, IPageDialogService dialogPage,
            ISalesService salesService,IVeterinaryService veterinaryService,IStoreService storeService,CreateAppointmentValidate validator) : base(navigationService, dialogPage)
        {

            _salesService = salesService;
            _veterinaryService = veterinaryService;
            _storeService = storeService;
            _validator = validator;
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());
            CreateAppointmentCommand = ReactiveCommand.CreateFromObservable<Sales>(CreateAppointment);
            var canExecuteAppointment = CreateAppointmentCommand.Select(e => e is {Orders: { }} && e.Orders.Any());//
            RequestAppointmentCommand =
                ReactiveCommand.CreateFromTask<Sales>((param) => NavigationService.NavigateAsync(ConstantUri.AppointmentComplete, new NavigationParameters()
                {
                    {ParameterConstant.OrderComplete,param}
                }),canExecuteAppointment);
            CreateAppointmentCommand
                .InvokeCommand(RequestAppointmentCommand);
     
        }

        public ReactiveCommand<Unit, Unit> NavigateGoBackCommand { get; set; }

        private IObservable<Sales> CreateAppointment()
        {
            _createAppointment.Date = Date;
            _createAppointment.Date = _createAppointment.Date.Date.Add(Time);
            _createAppointment.AssetServiceId = SelectedService?.AssetsServiceType;
            var validate = _validator.Validate(_createAppointment);
            if (!validate.IsValid)
            {
                ShowErrorMessage(validate.Errors.Select(e => e.ErrorMessage));
                return Observable.Return<Sales>(null);
            }
            return _salesService.CreateAppointment(_createAppointment);
        }

        protected override IObservable<Unit> ExecuteInitialize()
        {
           return  Observable.Create<Unit>((observer) =>
           {
    
                var disposable = new CompositeDisposable();
              var getData = _storeService.GetServicesByBusinessId(_createAppointment.BusinessId);
              _servicesAvailable = getData.WhereNotNull().ToProperty(this, x => x.ServicesAvailable);
              getData.Select(e =>  Unit.Default)
                    .Subscribe(observer);

                return disposable;
            });

        }

        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(ParameterConstant.VeterinaryPickerAppointment))
            {
                _createAppointment = parameters[ParameterConstant.VeterinaryPickerAppointment] as CreateAppointment;
                Date= (_createAppointment?.Date).GetValueOrDefault();
                Time = DateTime.Now.TimeOfDay;
            }
            if (parameters.ContainsKey(nameof(Veterinary)))
            {
                Veterinary = parameters[nameof(Veterinary)] as Veterinary;
            }
        
        }
    }
}