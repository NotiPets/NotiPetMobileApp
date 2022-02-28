using Prism.Navigation;
using Prism.Services;

namespace NotiPetApp.ViewModels
{
    public class VetTabViewModel:BaseViewModel
    {
        public VetTabViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
        }
    }
}