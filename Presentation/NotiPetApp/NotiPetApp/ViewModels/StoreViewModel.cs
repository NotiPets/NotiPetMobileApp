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
            var notificationParameters = _storeService.ParametersOptions.Connect().RefCount();
          
            notificationParameters
                .Filter(e=>e.IsActive)
                .Bind(out _parameterOptions)
                .DisposeMany().Subscribe()
                .DisposeWith(Subscriptions);
            
          var sortPredicate =  notificationParameters
                  .Filter(e=>e.IsSort)
                  .WhenPropertyChanged(e => e.IsActive,true)
              .Select(e =>
                  e.Value?  e.Sender.GetSortExpressions<SortExpressionComparer<AssetServiceModel>>()
                      :SortExpressionComparer<AssetServiceModel>.Descending(e=>e.Name))
              ;
          
          var filterPredicate =  notificationParameters
              .Filter(e=>!e.IsSort)
              .WhenPropertyChanged(e => e.IsActive,true)
              .Select(e => e.Value?  e.Sender.GetFilterExpression<Func<AssetServiceModel, bool>>() :e=>!string.IsNullOrEmpty(e.Name));
    
            var searchPredicate = this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromMilliseconds(500), RxApp.TaskpoolScheduler)
                .DistinctUntilChanged()
                .Select(SearchFunc);
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
            
            
        }
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
            });
        }
        public void Initialize(INavigationParameters parameters)
        {
            
        }

    }
}