using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using DynamicData.Kernel;
using NotiPet.Domain.Models;
using NotiPetApp.Helpers;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using Xamarin.Forms.Internals;

namespace NotiPetApp.ViewModels
{
    public class OptionsParametersViewModel:BaseViewModel,IInitialize
    {
        private SourceCache<ParameterOption, int> _sourceList;

        private  ReadOnlyObservableCollection<ObservableGroupingCollection<string, ParameterOption, int>>
            _parameterOptions;
        public ReadOnlyObservableCollection<ObservableGroupingCollection<string, ParameterOption, int>>
            ParameterOptions=>_parameterOptions;
        public ReactiveCommand<ParameterOption,Unit> ActiveFilterCommand { get; set; } 
        public ReactiveCommand<Unit,Unit> CloseCommand { get; } 
        public OptionsParametersViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
           

          CloseCommand = ReactiveCommand.CreateFromTask( GoBack);
           ActiveFilterCommand = ReactiveCommand.Create<ParameterOption>(ActiveFilter);
        }

      async  Task GoBack()
        {
         await   NavigationService.GoBackAsync();
        }
        private void ActiveFilter(ParameterOption parameterOption)
        {
            var value = _sourceList.Items
                .FirstOrDefault(e =>e.Key==parameterOption.Key&&e.Id!=parameterOption.Id&&e.IsActive);
            _sourceList.Edit((update) =>
            {
                if (value!=null)
                {
                    value.SetActive(false);
                    update.AddOrUpdate(parameterOption);
                }
                parameterOption.SetActive(!parameterOption.IsActive);
                update.AddOrUpdate(parameterOption);
                
            });
            _sourceList.Refresh();
        }

        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(ParameterConstant.OptionsParameter)
                &&parameters[ParameterConstant.OptionsParameter] is SourceCache<ParameterOption, int> options)
            {
                _sourceList = options;
                var notificationsParameters =    _sourceList.Connect().RefCount();
                notificationsParameters
                    .Group(e=>e.Key)
                    .Transform(x=>new ObservableGroupingCollection<string,ParameterOption,int>(x))
                    .Sort(SortExpressionComparer<ObservableGroupingCollection<string,ParameterOption,int>>.Descending(x=>x.Key))
                    .Bind(out _parameterOptions)
                       .DisposeMany()
                    .Subscribe()
                    .DisposeWith(Subscriptions);
            }
           
        }
    }
}