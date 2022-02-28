using System;
using System.Collections.ObjectModel;
using System.Reactive;
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
    public class VeterinaryViewModel:BaseViewModel
    {
        private readonly IVeterinaryService _veterinaryService;
        private readonly ISchedulerProvider _schedulerProvider;
        private readonly ReadOnlyObservableCollection<Veterinary> _veterinaries;


        public VeterinaryViewModel(INavigationService navigationService, IPageDialogService dialogPage,
            IVeterinaryService veterinaryService, ISchedulerProvider schedulerProvider) : base(navigationService, dialogPage)
        {
            _veterinaryService = veterinaryService;
            _schedulerProvider = schedulerProvider;
          var searchPredicate = this.WhenAnyValue(e => e.SearchText)
                .Throttle(TimeSpan.FromMilliseconds(100), _schedulerProvider.CurrentThread)
                .DistinctUntilChanged()
                .Select(_veterinaryService.SearchPredicate);
            _veterinaryService.Veterinaries.Connect()
                .ObserveOn(_schedulerProvider.MainThread)
                .Filter(searchPredicate)
                .Bind(out _veterinaries)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);
            InitializingCommand = ReactiveCommand.CreateFromObservable(InitializeData);
        }

        private IObservable<Unit> InitializeData()
        {
            return _veterinaryService.GetVeterinary()
                .Select(e => Unit.Default);
        }
        public ReadOnlyObservableCollection<Veterinary> Veterinaries => _veterinaries;
        public ReactiveCommand<Unit,Unit> InitializingCommand { get;  }
      [Reactive]  public string SearchText { get; set; }
    }
}