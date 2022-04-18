using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using DynamicData.PLinq;
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
    public class SpecialistViewModel:BaseViewModel
    {

        private readonly ISpecialistsService _specialistsService;
  
        private readonly ReadOnlyObservableCollection<Specialist> _specialists;
        public ReadOnlyObservableCollection<Specialist> Specialists => _specialists;
        public ReadOnlyObservableCollection<ParameterOption> _parameterOptions;
        public ReadOnlyObservableCollection<ParameterOption> ParameterOptions=>_parameterOptions;
        public ReactiveCommand<string,Unit> ShowDetailCommand  { get; set; }
        public SpecialistViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
            ISpecialistsService specialistsService,
            ISchedulerProvider schedulerProvider) : base(navigationService,pageDialogService)
        {
            _specialistsService = specialistsService;
            var notificationParameters = _specialistsService.ParametersOptions
                .Connect()
                .RefCount();
            var sortPredicate =  notificationParameters
                .WhenPropertyChanged(e => e.IsActive,true)
                .Where(e=>e.Value&&e.Sender.IsSort)
                .Select(e =>
                    e.Value?  e.Sender.GetSortExpressions<SortExpressionComparer<Specialist>>()
                        :SortExpressionComparer<Specialist>.Descending(e=>e.User.Names));

            var defaultFilter =
                new PropertyValue<ParameterOption, bool>(ParameterOption.Default
                    .SetFilterExpression<Specialist>(x => !string.IsNullOrEmpty(x.User.Names)), true);
          
            var filterPredicate = notificationParameters
                .WhenPropertyChanged(e => e.IsActive, false)
                .StartWith(defaultFilter)
                .Throttle(TimeSpan.FromMilliseconds(500), schedulerProvider.CurrentThread)
                .DistinctUntilChanged()
                .Where(x=>!x.Sender.IsSort)
                .Select(e=> (e.Value)?e:defaultFilter)
                .Select(e => e.Sender.GetFilterExpression<Func<Specialist, bool>>());
          
            var searchPredicate = this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromMilliseconds(500), schedulerProvider.CurrentThread)
                .DistinctUntilChanged()
                .Select(SearchFunc);
            notificationParameters
                .Filter(e=>e.IsActive)
                .Bind(out _parameterOptions)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);

            _specialistsService.SpecialistSource
                .Connect()
                .Filter(filterPredicate)
                .Filter(searchPredicate)
                .Sort(sortPredicate)
                .ObserveOn(schedulerProvider.MainThread)
                .Bind(out _specialists)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);

            _isBusy = InitializeCommand
                        .IsExecuting
                        .ToProperty(this, x => x.IsBusy);
            NavigateToFilterCommand = ReactiveCommand.CreateFromTask(NavigateToFilter);
            ShowDetailCommand = ReactiveCommand.CreateFromTask<string>(ShowDetail);

        }

         Task ShowDetail(string id)
        {
          return  NavigationService.NavigateAsync(ConstantUri.ShowSpecialists,new NavigationParameters()
          {
              {ParameterConstant.SpecialistId,id}
          } );
        }
        async   Task NavigateToFilter()
        {
            await NavigationService.NavigateAsync(ConstantUri.OptionParameters,new NavigationParameters()
            {
                {ParameterConstant.OptionsParameter,_specialistsService.ParametersOptions}
            },true);
        }
        public ReactiveCommand<Unit, Unit> NavigateToFilterCommand { get; set; }
        Func<Specialist, bool> SearchFunc(string text) =>
            model => string.IsNullOrEmpty(text) || model.User.Names.ToLower().Contains(text.ToLower());
        [Reactive] public string SearchText { get; set; }

        protected override IObservable<Unit> ExecuteInitialize()
        {
            return Observable.Create<Unit>(observer =>
            {
                var disposable = new CompositeDisposable();
                 _specialistsService.GetSpecialists()
                    .Select(e => Unit.Default)
                    .Subscribe(observer);
                 _specialistsService.ParameterOptions().Select(e => Unit.Default)
                     .Subscribe()
                     .DisposeWith(disposable);
                 return disposable;
            });
        }
    }
}