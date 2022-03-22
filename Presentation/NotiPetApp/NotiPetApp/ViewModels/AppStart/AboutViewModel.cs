using Prism.Navigation;
using Prism.Services;

namespace NotiPetApp.ViewModels
{
    public class AboutViewModel:BaseViewModel
    {
        public AboutViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
        }
    }
}