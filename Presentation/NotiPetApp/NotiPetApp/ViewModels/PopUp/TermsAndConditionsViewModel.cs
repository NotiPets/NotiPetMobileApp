using System.Reactive;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace NotiPetApp.ViewModels.PopUp
{
    public class TermsAndConditionsViewModel:BaseViewModel
    {
        public string Text { get; set; }
        public TermsAndConditionsViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());
        }

        public ReactiveCommand<Unit, Unit> NavigateGoBackCommand { get;  }
    }
}