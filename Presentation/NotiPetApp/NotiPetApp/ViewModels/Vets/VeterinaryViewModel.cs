using Prism.Navigation;
using Prism.Services;

namespace NotiPetApp.ViewModels
{
    public class VeterinaryViewModel:BaseViewModel
    {
        public VeterinaryViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
        }
    }
}