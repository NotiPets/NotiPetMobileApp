using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using NotiPet.Data.Dtos;
using NotiPet.Data.Services;
using NotiPet.Domain.Models;
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
       public ObservableCollection<ParameterOption> ParameterOptions { get; set; }
       public ReadOnlyObservableCollection<AssetServiceModel> Products=>_assetService;
        public StoreViewModel(INavigationService navigationService, 
            IPageDialogService dialogPage,IStoreService storeService) : base(navigationService, dialogPage)
        {
            _storeService = storeService;
            ParameterOptions = new ObservableCollection<ParameterOption>(storeService.ParameterOptions());
            var searchPredicate = this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromMilliseconds(500), RxApp.TaskpoolScheduler)
                .DistinctUntilChanged()
                .Select(SearchFunc);
            _storeService.AssetsServices
                .Connect()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Filter(searchPredicate)
                .Bind(out _assetService)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);
                InitializeDataCommand  = ReactiveCommand.CreateFromObservable(InitializeData);
            
            
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
                    return disposable;
                });


        public void Initialize(INavigationParameters parameters)
        {
            InitializeDataCommand.Execute()
                .Subscribe()
                .DisposeWith(Subscriptions);
            
        }
        
    }
}