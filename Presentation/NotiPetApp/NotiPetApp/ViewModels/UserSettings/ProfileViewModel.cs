
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using NotiPetApp.Helpers;
using NotiPetApp.Models;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace NotiPetApp.ViewModels
{
    public class ProfileViewModel:BaseViewModel
    {
        public string test { get; set; }
        public ProfileViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
            test = "hola";
            AppMenuItems = new ObservableCollection<AppMenuItem>()
            {
                new(ConstantDictionary.MyPets,"patas",1,ReactiveCommand.CreateFromTask(NavigateToMyPets),SizeItem.Small),
                new(ConstantDictionary.About,"question",2,ReactiveCommand.CreateFromTask(NavigateToAbout),SizeItem.Small),
                new(ConstantDictionary.Settings,"settings",3,ReactiveCommand.CreateFromTask(NavigateToSettings),SizeItem.Small),
                new(ConstantDictionary.Logout,"logOut",4,ReactiveCommand.CreateFromTask(LogOut),SizeItem.Small),
            };
        }

        Task NavigateToMyPets()
        {
            return NavigationService.NavigateAsync(ConstantUri.MyPets);
        }
        Task NavigateToAbout()
        {
            return NavigationService.NavigateAsync(ConstantUri.About);
        }
        Task NavigateToSettings()
        {
            return NavigationService.NavigateAsync(ConstantUri.Settings);
        }
       public ObservableCollection<AppMenuItem> AppMenuItems { get; }

        Task LogOut()
        {
         return   NavigationService.NavigateAsync(ConstantUri.SocialNetworkAuthentication);
        }
    }
}