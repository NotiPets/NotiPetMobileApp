using Prism.Navigation;
using Prism.Services;

namespace NotiPetApp.ViewModels
{
    public class OptionsParametersViewModel:BaseViewModel
    {
        public OptionsParametersViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
        }
    }
}