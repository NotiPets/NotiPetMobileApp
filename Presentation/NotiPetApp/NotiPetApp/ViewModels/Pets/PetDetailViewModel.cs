using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using DynamicData;
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
        public Pet Pet { get; set; }
        private ReadOnlyObservableCollection<Vaccinate> _vaccinates;
        public ReadOnlyObservableCollection<Vaccinate> Vaccinates => _vaccinates;
        public ReactiveCommand<Unit, Unit> NavigateGoBackCommand { get; set; }
        public PetDetailViewModel(INavigationService navigationService, IPageDialogService dialogPage,IPetsService petsService,ISchedulerProvider schedulerProvider) : base(navigationService, dialogPage)
        {
            _petsService = petsService;
            _schedulerProvider = schedulerProvider;
            _petsService.Vaccinate.Connect()
                .ObserveOn(_schedulerProvider.CurrentThread)
                .Bind(out _vaccinates);

            ShowVaccinatePdfCommand = ReactiveCommand.CreateFromTask<Pet>((param, token)
                => NavigationService.NavigateAsync(ConstantUri.ShowVaccines, new NavigationParameters()
                {
                    {ParameterConstant.Vacinnes, _vaccinates}
                }));
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());
        }

        public ReactiveCommand<Pet, Unit> ShowVaccinatePdfCommand { get; }

        protected override IObservable<Unit> ExecuteInitialize()
        {
          return  _petsService.GetVaccinesByPet(Pet.Id).Select(x=>Unit.Default);
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