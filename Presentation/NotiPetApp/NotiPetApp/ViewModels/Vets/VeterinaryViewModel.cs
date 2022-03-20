using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
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
    public class VeterinaryViewModel:BaseViewModel
    {
        private readonly IVeterinaryService _veterinaryService;
        private readonly ReadOnlyObservableCollection<Veterinary> _veterinaries;
        public ReadOnlyObservableCollection<ParameterOption> _parameterOptions;
        public ReadOnlyObservableCollection<ParameterOption> ParameterOptions=>_parameterOptions;
        public VeterinaryViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IVeterinaryService veterinaryService, ISchedulerProvider schedulerProvider) : base(navigationService,pageDialogService)
        {
            _veterinaryService = veterinaryService;

            var notificationParameters = _veterinaryService.ParametersOptions
                .Connect()
                .RefCount();
            var sortPredicate =  notificationParameters
                .WhenPropertyChanged(e => e.IsActive,true)
                .Where(e=>e.Value&&e.Sender.IsSort)
                .Select(e =>
                    e.Value?  e.Sender.GetSortExpressions<SortExpressionComparer<Veterinary>>()
                        :SortExpressionComparer<Veterinary>.Descending(e=>e.Name));

            var defaultFilter =
                new PropertyValue<ParameterOption, bool>(ParameterOption.Default
                    .SetFilterExpression<AssetServiceModel>(x => x.Active), true);
          
            var filterPredicate = notificationParameters
                .WhenPropertyChanged(e => e.IsActive, false)
                .StartWith(defaultFilter)
                .Throttle(TimeSpan.FromMilliseconds(500), RxApp.TaskpoolScheduler)
                .DistinctUntilChanged()
                .Where(x=>!x.Sender.IsSort)
                .Select(e=> (e.Value)?e:defaultFilter)
                .Select(e => e.Sender.GetFilterExpression<Func<Veterinary, bool>>());
          
            var searchPredicate = this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromMilliseconds(500), RxApp.TaskpoolScheduler)
                .DistinctUntilChanged()
                .Select(SearchFunc);
            notificationParameters
                .Filter(e=>e.IsActive)
                .Bind(out _parameterOptions)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);
            searchPredicate
                .Concat(filterPredicate);
           
            _veterinaryService.Veterinaries.Connect()
                .ObserveOn(schedulerProvider.MainThread)
                .Filter(searchPredicate)
                .Sort(sortPredicate)
                .Bind(out _veterinaries)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);
            NavigateToFilterCommand = ReactiveCommand.CreateFromTask(NavigateToFilter);
        }
        Func<Veterinary, bool> SearchFunc(string text) =>
            model => string.IsNullOrEmpty(text) || model.Name.ToLower().Contains(text.ToLower());
       [Reactive] public string SearchText { get; set; }

        async   Task NavigateToFilter()
        {
            await NavigationService.NavigateAsync(ConstantUri.OptionParameters,new NavigationParameters()
            {
                {ParameterConstant.OptionsParameter,_veterinaryService.ParametersOptions}
            },true);
        }
        public ReactiveCommand<Unit, Unit> NavigateToFilterCommand { get; set; }

        protected override IObservable<Unit> ExecuteInitialize()
        {
            return Observable.Create<Unit>(observer =>
            {
                var disposable = new CompositeDisposable();
                 _veterinaryService.GetVeterinary()
                    .Select(e => Unit.Default)
                    .Subscribe(observer)
                    .DisposeWith(disposable);
                 _veterinaryService.ParameterOptions().Select(e => Unit.Default)
                    .Subscribe(observer)
                    .DisposeWith(disposable);
                return disposable;
            });
        }

        public ReadOnlyObservableCollection<Veterinary> Veterinaries => _veterinaries;
    }
}