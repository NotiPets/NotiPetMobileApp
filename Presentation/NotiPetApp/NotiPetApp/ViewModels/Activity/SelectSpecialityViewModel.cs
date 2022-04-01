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
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NotiPetApp.ViewModels.Activity
{
    public class SelectSpecialityViewModel:BaseViewModel
    {
        [Reactive] public DateTime Date { get; set; }
        [Reactive]  public Speciality SelectSepSpeciality { get; set; }
        private readonly ISpecialistsService _specialistsService;
        private ReadOnlyObservableCollection<Speciality> _specialities;
        public CreateAppointment Appointment { get; set; }
        public ReactiveCommand<Unit,Unit> ContinueCommand { get; }
        public SelectSpecialityViewModel(INavigationService navigationService, IPageDialogService dialogPage,
            ISpecialistsService specialistsService, ISchedulerProvider schedulerProvider) : base(navigationService, dialogPage)
        {
            Appointment = new CreateAppointment();
            _specialistsService = specialistsService;
            specialistsService.SpecialitySource.Connect()
                .ObserveOn(schedulerProvider.MainThread)
                .Bind(out _specialities)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);
            ContinueCommand = ReactiveCommand.CreateFromTask(Continue);
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());

        }

        public ReactiveCommand<Unit, Unit> NavigateGoBackCommand { get; set; }

        Task Continue()
        {
            return NavigationService.NavigateAsync(ConstantUri.ShowSpecialists,parameters:new NavigationParameters()
            {
                {nameof(CreateAppointment),Appointment}
            });
        }

        protected override IObservable<Unit> ExecuteInitialize()
        {
            return Observable.Create<Unit>(_ => _specialistsService.GetSpecialities()
                .Subscribe());
        }

        public ReadOnlyObservableCollection<Speciality> Specialities => _specialities;
    }
}