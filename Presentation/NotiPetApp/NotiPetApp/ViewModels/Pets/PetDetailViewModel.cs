using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Bogus.DataSets;
using DynamicData;
using DynamicData.PLinq;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using NotiPetApp.Services;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace NotiPetApp.ViewModels
{
    public class PetDetailViewModel:BaseViewModel,IInitialize
    {
        private readonly IPetsService _petsService;
        private readonly ISchedulerProvider _schedulerProvider;
        private readonly ISalesService _salesService;
        public Pet Pet { get; set; }
        public IEnumerable<DigitalVaccine> Vaccinates => _vaccinates?.Value;
        private ReadOnlyObservableCollection<AppointmentSale> _sales;
        public ReadOnlyObservableCollection<AppointmentSale> Sales => _sales;
        private  ObservableAsPropertyHelper<IEnumerable<DigitalVaccine>> _vaccinates;
        public ReactiveCommand<Unit, Unit> NavigateGoBackCommand { get; set; }
        public ReactiveCommand<Unit, Unit> CreateAppointmentCommand { get; set; }
        public ReactiveCommand<DigitalVaccine, Unit> ShowVaccinatePdfCommand { get; }
        public PetDetailViewModel(INavigationService navigationService, IPageDialogService dialogPage,IPetsService petsService,ISchedulerProvider schedulerProvider,ISalesService salesService) : base(navigationService, dialogPage)
        {
            _petsService = petsService;
            _schedulerProvider = schedulerProvider;
            _salesService = salesService;
            _salesService.DataSource.Connect()
                .ObserveOn(_schedulerProvider.CurrentThread)
                .Filter(x=>x.PetId== Pet?.Id)
                .Bind(out _sales)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);
            ShowVaccinatePdfCommand = ReactiveCommand.CreateFromTask<DigitalVaccine>((param, token)
                => NavigationService.NavigateAsync(ConstantUri.ShowVaccines, new NavigationParameters()
                {
                    {ParameterConstant.Vacinnes, param}
                },true));
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());
            CreateAppointmentCommand = ReactiveCommand.CreateFromTask(CreateAppointment);
        }
        protected override IObservable<Unit> ExecuteInitialize()
            => Observable.Create<Unit>(observable =>
        {
            var disposable = new CompositeDisposable();
            _salesService.GetSaleByUserId(Settings.UserId)
                .Select(e => Unit.Default)
                .Subscribe()
                .DisposeWith(disposable);

            var getVaccines = _petsService
                .GetVaccinesByPet(Pet.Id);
            _vaccinates =   getVaccines .ToProperty(this,x=>x.Vaccinates);
            getVaccines .Select(x=>Unit.Default)
                .Subscribe()
                .DisposeWith(disposable);
            return disposable;
        });
        Task CreateAppointment()
        {
            //VetTabViewModel
            var appointment  = new CreateAppointment(DateTime.Now, Pet?.Id,Settings.UserId);
            return NavigationService.NavigateAsync(ConstantUri.VeterinaryPickerPage,parameters:new NavigationParameters()
            {
                {ParameterConstant.VeterinaryPickerAppointment,appointment}
            });
        }
        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(ParameterConstant.Pet))
            {
                Pet = parameters[ParameterConstant.Pet]  as Pet;

            }
        }
    }
}