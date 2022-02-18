
using Prism.Navigation;
using Prism.Services;

namespace NotiPetApp.ViewModels
{
    public class ProfileViewModel:BaseViewModel
    {
        public ProfileViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
        }
    }
}