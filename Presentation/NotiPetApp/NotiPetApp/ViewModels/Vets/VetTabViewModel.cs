using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.PLinq;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using NotiPetApp.Models;
using NotiPetApp.Services;
using NotiPetApp.Views.Vets;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NotiPetApp.ViewModels
{
    public class VetTabViewModel:BaseViewModel
    {
        private readonly IVeterinaryService _veterinaryService;
        private readonly ISchedulerProvider _schedulerProvider;
        private readonly ISpecialistsService _specialistsService;
        private int _selectedIndex;
      [Reactive]  public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;

            }
        }
      public ObservableAsPropertyHelper<object> _tabItem;
      public object TabItem=>_tabItem.Value;

      public ObservableCollection<TabItemReactive> TabItemCollections { get; set; }
      public ReactiveCommand<int,Unit> LoadDataCommand { get; set; }

      public VetTabViewModel(INavigationService navigationService, IPageDialogService dialogPage,
          IVeterinaryService veterinaryService, ISchedulerProvider schedulerProvider,
          ISpecialistsService specialistsService) : base(navigationService, dialogPage)
      {
          _veterinaryService = veterinaryService;
          _schedulerProvider = schedulerProvider;
          _specialistsService = specialistsService;
          TabItemCollections = new ObservableCollection<TabItemReactive>()
          {
               TabItemReactive.CreateReactive<SpecialistView,SpecialistViewModel>("Specialists"),
               TabItemReactive.CreateReactive<VeterinaryView,VeterinaryViewModel>("Veterinaries"),
          };

          NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());

          _tabItem = this.WhenAnyValue(x => x.SelectedIndex)
              .Select(e => TabItemCollections[e])
              .Do(x =>
              {
                  var tabSelected = TabItemCollections.FirstOrDefault(e => e.IsSelected);
                  tabSelected?.SetSelected(false);
                  x.SetSelected(!x.IsSelected);
              })
              .ToProperty(this, x => x.TabItem);

      }

      public ReactiveCommand<Unit,Unit> NavigateGoBackCommand { get; set; }



    }
}