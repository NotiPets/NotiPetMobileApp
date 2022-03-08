using Prism.Navigation;
using Prism.Services;

namespace NotiPetApp.ViewModels
{
    public class TabMenuViewModel:BaseViewModel
    {
        public TabMenuViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
        }
    }
}