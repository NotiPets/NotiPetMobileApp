using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using NotiPetApp.Services;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NotiPetApp.ViewModels.Activity
{
    public class SelectSpecialityViewModel:BaseViewModel
    {
        [Reactive] public DateTime Date { get; set; }
        public CreateAppointment Appointment { get; set; }
        public ReactiveCommand<Unit,Unit> ContinueCommand { get; }

        private readonly IPetsService _petsService;
        private Pet _selectedItem;

        [Reactive] public Pet SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;
        }

        private Pet lastPet;
        private readonly  ReadOnlyObservableCollection<Pet> _pets;
        public ReadOnlyObservableCollection<Pet> Pets => _pets;
        private ObservableAsPropertyHelper<bool> _canContinue;
        public bool CanContinue => _canContinue.Value;
        public SelectSpecialityViewModel(INavigationService navigationService, IPageDialogService dialogPage,
            ISchedulerProvider schedulerProvider,IPetsService petsService) : base(navigationService, dialogPage)
        {
           
            Date= DateTime.Now;
            ContinueCommand = ReactiveCommand.CreateFromTask(Continue);
            _petsService = petsService;
            _petsService.Pets.Connect()
                .ObserveOn(schedulerProvider.CurrentThread)
                .Bind(out _pets)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);
            _isBusy =  ContinueCommand.IsExecuting.ToProperty(this,x=>x.IsBusy);
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());
            var selectItemObservable = this.WhenAnyValue(x => x.SelectedItem)
                ;
            _canContinue  =  selectItemObservable.Select(x => x != null).ToProperty(this, x => x.CanContinue);
            selectItemObservable.Subscribe(x =>
            {
                lastPet?.SetIsSelected(false);
                x?.SetIsSelected(true);
                lastPet = x;
            }).DisposeWith(Subscriptions);
        }




        public ReactiveCommand<Unit, Unit> NavigateGoBackCommand { get; set; }

        Task Continue()
        {
            //VetTabViewModel
            Appointment = new CreateAppointment(Date,SelectedItem?.Id,Settings.UserId);
            return NavigationService.NavigateAsync(ConstantUri.VeterinaryPickerPage,parameters:new NavigationParameters()
            {
                {ParameterConstant.VeterinaryPickerAppointment,Appointment}
            });
        }

        protected override IObservable<Unit> ExecuteInitialize()
            => _petsService.GetPets(Settings.UserId).Select(e => Unit.Default);
    }
}