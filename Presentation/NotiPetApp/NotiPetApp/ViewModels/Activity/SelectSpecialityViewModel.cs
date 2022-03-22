using Prism.Navigation;
using Prism.Services;

namespace NotiPetApp.ViewModels.Activity
{
    public class SelectSpecialityViewModel:BaseViewModel
    {
        public SelectSpecialityViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
        }
    }
}