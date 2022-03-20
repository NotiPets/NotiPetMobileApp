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

namespace NotiPetApp.ViewModels
{
    public class SpecialistViewModel:BaseViewModel
    {

        private readonly ISpecialistsService _specialistsService;
  
        private readonly ReadOnlyObservableCollection<Specialist> _specialists;
        public ReadOnlyObservableCollection<Specialist> Specialists => _specialists;

        public SpecialistViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
            ISpecialistsService specialistsService,
            ISchedulerProvider schedulerProvider) : base(navigationService,pageDialogService)
        {
            _specialistsService = specialistsService;
            _specialistsService.SpecialistSource
                .Connect()
                .ObserveOn(schedulerProvider.MainThread)
                .Bind(out _specialists)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);

            _isBusy = InitializeCommand
                        .IsExecuting
                        .ToProperty(this, x => x.IsBusy);

        }

        protected override IObservable<Unit> ExecuteInitialize()
        {
            return Observable.Create<Unit>(observer =>
            {
                return _specialistsService.GetSpecialists()
                    .Select(e => Unit.Default)
                    .Subscribe(observer);
            });
        }
    }
}