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
        private SourceCache<ParameterOption, string> _sourceList;

        private  ReadOnlyObservableCollection<ObservableGroupingCollection<string, ParameterOption, string>>
            _parameterOptions;
        public ReadOnlyObservableCollection<ObservableGroupingCollection<string, ParameterOption, string>>
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
            _sourceList.Edit((update) =>
            {
                parameterOption.SetActive(!parameterOption.IsActive);
                update.AddOrUpdate(parameterOption);
                
            });
            _sourceList.Refresh();
        }

        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(ParameterConstant.OptionsParameter)
                &&parameters[ParameterConstant.OptionsParameter] is SourceCache<ParameterOption, string> options)
            {
                _sourceList = options;
                var notificationsParameters =    _sourceList.Connect().RefCount();
                notificationsParameters
                    .Group(e=>e.Key)
                    .Transform(x=>new ObservableGroupingCollection<string,ParameterOption,string>(x))
                    .Bind(out _parameterOptions)
                    .DisposeMany()
                    .Subscribe()
                    .DisposeWith(Subscriptions);
            }
           
        }
    }
}