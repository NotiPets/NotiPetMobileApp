using Prism.Navigation;
using Prism.Services;

namespace NotiPetApp.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {
        public LoginViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
        }
    }
}