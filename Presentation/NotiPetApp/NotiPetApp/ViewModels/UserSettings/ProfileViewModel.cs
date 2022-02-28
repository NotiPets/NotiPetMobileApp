
using System.Reactive;
using System.Threading.Tasks;
using NotiPetApp.Helpers;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace NotiPetApp.ViewModels
{
    public class ProfileViewModel:BaseViewModel
    {
        public ReactiveCommand<Unit,Unit> LogOutCommand { get; set; }
        public ProfileViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
            LogOutCommand = ReactiveCommand.CreateFromTask(LogOut);
        }

        Task LogOut()
        {
         return   NavigationService.NavigateAsync(ConstantUri.SocialNetworkAuthentication);
        }
    }
}