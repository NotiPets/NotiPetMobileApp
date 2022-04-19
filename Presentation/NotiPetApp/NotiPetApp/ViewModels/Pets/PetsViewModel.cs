using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using NotiPetApp.Services;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NotiPetApp.ViewModels
{
    public class PetsViewModel:BaseViewModel
    {
        private readonly IPetsService _petsService;
        private readonly  ReadOnlyObservableCollection<Pet> _pets;
        public ReadOnlyObservableCollection<Pet> Pets => _pets;
       public ReactiveCommand<Unit,Unit> NavigateToRegisterPetCommand{ get; set; }


        public PetsViewModel(INavigationService navigationService, IPageDialogService dialogPage,
            IPetsService petsService,    ISchedulerProvider currentThread) : base(navigationService, dialogPage)
        {
            _petsService = petsService;
            _petsService.Pets.Connect()
                .ObserveOn(currentThread.CurrentThread)
                .Bind(out _pets)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());
            NavigateToRegisterPetCommand = ReactiveCommand.CreateFromTask<Unit>((param) => NavigationService.NavigateAsync(ConstantUri.RegisterOrEditPet));


        }

        public ReactiveCommand<Unit, Unit> NavigateGoBackCommand { get; set; }
        protected override IObservable<Unit> ExecuteInitialize()
            => _petsService.GetPets(Settings.UserId).Select(e => Unit.Default);



    }
}