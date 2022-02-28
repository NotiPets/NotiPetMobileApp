using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
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
        public ReactiveCommand<Unit,Unit> InitializingCommand { get; set; }
        private readonly ReadOnlyObservableCollection<Pet> _pets;
        public ReadOnlyObservableCollection<Pet> Pets => _pets;
       [Reactive] public string SearchText { get; set; }

        public PetsViewModel(INavigationService navigationService, IPageDialogService dialogPage,
            IPetsService petsService,    ISchedulerProvider currentThread) : base(navigationService, dialogPage)
        {
            _petsService = petsService;
            var filterSearch = this.WhenAnyValue(e => e.SearchText)
                .Throttle(TimeSpan.FromMilliseconds(100), scheduler:currentThread.CurrentThread)
                .DistinctUntilChanged()
                .Select(_petsService.SearchPredicate);
            _petsService.Pets.Connect()
                .ObserveOn(currentThread.MainThread)
                .Filter(filterSearch)
                .Bind(out _pets)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);
            InitializingCommand = ReactiveCommand.CreateFromObservable(Initialize);
            

        }

        IObservable<Unit> Initialize()
            => _petsService.GetPets().Select(e => Unit.Default);
    }
}