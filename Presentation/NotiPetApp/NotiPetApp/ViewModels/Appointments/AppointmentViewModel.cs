using Prism.Navigation;
using Prism.Services;

namespace NotiPetApp.ViewModels
{
    public class AppointmentViewModel:BaseViewModel
    {
        public AppointmentViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
        }
    }
}