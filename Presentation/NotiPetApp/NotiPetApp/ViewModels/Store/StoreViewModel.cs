using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using DynamicData.PLinq;
using NotiPet.Data.Dtos;
using NotiPet.Data.Services;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using NotiPetApp.Styles.Fonts;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms.Internals;

namespace NotiPetApp.ViewModels
{
    public class StoreViewModel:BaseViewModel,IInitialize
    {
   
        private readonly IStoreService _storeService;
        private ReadOnlyObservableCollection<AssetServiceModel> _assetService;
        private string _searchText;

       [Reactive]  public string SearchText
        {
            get => _searchText;
            set => _searchText = value;
        }

       public ReactiveCommand<Unit,Unit> InitializeDataCommand { get; set; }
       public ReadOnlyObservableCollection<ParameterOption> _parameterOptions;
       public ReadOnlyObservableCollection<ParameterOption> ParameterOptions=>_parameterOptions;
       public ReadOnlyObservableCollection<AssetServiceModel> Products=>_assetService;
       public ReactiveCommand<Unit,Unit> NavigateToFilterCommand { get; set; }
        public StoreViewModel(INavigationService navigationService, 
            IPageDialogService dialogPage,IStoreService storeService) : base(navigationService, dialogPage)
        {
            _storeService = storeService;
            var notificationParameters = _storeService.ParametersOptions
                .Connect()
                .RefCount();

          var sortPredicate =  notificationParameters
                  .WhenPropertyChanged(e => e.IsActive,true)
                  .Where(e=>e.Value&&e.Sender.IsSort)
                  .Select(e =>
                  e.Value?  e.Sender.GetSortExpressions<SortExpressionComparer<AssetServiceModel>>()
                      :SortExpressionComparer<AssetServiceModel>.Descending(e=>e.Name));

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
              .Select(e => e.Sender.GetFilterExpression<Func<AssetServiceModel, bool>>());
          
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

            _storeService.AssetsServices
                .Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Filter(filterPredicate)
                .Filter(searchPredicate)
                .Sort(sortPredicate)
                .Bind(out _assetService)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);
            
                InitializeDataCommand  = ReactiveCommand.CreateFromObservable(InitializeData);
                NavigateToFilterCommand = ReactiveCommand.CreateFromTask(NavigateToFilter);
                NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b,token) =>   NavigationService.GoBackAsync());
            
            
        }

        public ReactiveCommand<Unit, Unit> NavigateGoBackCommand { get;  }

        Func<AssetServiceModel, bool> SearchFunc(string text) =>
            model => string.IsNullOrEmpty(text) || model.Name.ToLower().Contains(text.ToLower());
        IObservable<Unit> InitializeData() => Observable.Create<Unit>(observable =>
                {
                    var disposable = new CompositeDisposable();
                    _storeService.GetAllProducts()
                        .Select(e => Unit.Default)
                        .Subscribe()
                        .DisposeWith(disposable);
                    _storeService.ParameterOptions().Select(e => Unit.Default)
                        .Subscribe()
                        .DisposeWith(disposable);
                    return disposable;
                });

        async   Task NavigateToFilter()
        {
            await NavigationService.NavigateAsync(ConstantUri.OptionParameters,new NavigationParameters()
            {
                {ParameterConstant.OptionsParameter,_storeService.ParametersOptions}
            },true);
        }
        public void Initialize(INavigationParameters parameters)
        {
            
        }

    }
}