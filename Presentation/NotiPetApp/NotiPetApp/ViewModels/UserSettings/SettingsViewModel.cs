using Prism.Navigation;
using Prism.Services;

namespace NotiPetApp.ViewModels
{
    public class SettingsViewModel:BaseViewModel
    {
        public SettingsViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
        }
    }
}