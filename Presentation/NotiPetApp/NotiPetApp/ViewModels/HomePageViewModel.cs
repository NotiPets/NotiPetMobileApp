using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DynamicData;
using NotiPetApp.Helpers;
using NotiPetApp.Models;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace NotiPetApp.ViewModels
{
    public class HomeViewModel:BaseViewModel
    {
        public ObservableCollection<AppMenuItem> AppMenuItems { get; set; }
        public HomeViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
            AppMenuItems = new ObservableCollection<AppMenuItem>()
            {
                new(ConstantDictionary.Veterinary,"Veterinary",1,ReactiveCommand.CreateFromTask(NavigateToLastVisitVeterinary),SizeItem.Small),
                new(ConstantDictionary.Bathe,"Shower",2,ReactiveCommand.CreateFromTask(NavigateToActivity),SizeItem.Small),
                new(ConstantDictionary.Store,"Shop",3,ReactiveCommand.CreateFromTask(ShowNavigateToStory),SizeItem.Small),
                new(ConstantDictionary.Emergency,"Emergency",4,ReactiveCommand.CreateFromTask(CallEmergency),SizeItem.Large),
            };
        }

        async Task NavigateToLastVisitVeterinary()
        {
          await  NavigationService.NavigateAsync(ConstantUri.Veterinary);
        }
        async Task NavigateToActivity()
        {
            await  NavigationService.NavigateAsync(ConstantUri.Activity);
        }
        async Task ShowNavigateToStory()
        {
            await  NavigationService.NavigateAsync(ConstantUri.Store);
        }
        async Task CallEmergency()
        {
           //TODO Call Emergency
        }
    }
}