using Prism.Navigation;
using Prism.Services;

namespace NotiPetApp.ViewModels
{
    public class CreatePetViewModel:BaseViewModel
    {
        public CreatePetViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
        }
    }
}