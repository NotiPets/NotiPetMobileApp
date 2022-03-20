using System.Reactive;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace NotiPetApp.ViewModels
{
    public class AppointmentViewModel:BaseViewModel
    {
        
        public AppointmentViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b,token) =>   NavigationService.GoBackAsync());
        }

        public ReactiveCommand<Unit,Unit> NavigateGoBackCommand { get; set; }
    }
}