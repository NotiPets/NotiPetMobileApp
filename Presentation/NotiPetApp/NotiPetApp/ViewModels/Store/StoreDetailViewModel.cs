using Prism.Navigation;
using Prism.Services;

namespace NotiPetApp.ViewModels
{
    public class StoreDetailViewModel : BaseViewModel
    {
        public StoreDetailViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
        }
    }
}