using Prism.Navigation;
using Prism.Services;

namespace NotiPetApp.ViewModels
{
    public class StartViewModel:BaseViewModel
    {
        public StartViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
        }
    }
}