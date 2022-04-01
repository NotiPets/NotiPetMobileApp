using System.Reactive;
using NotiPetApp.Helpers;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace NotiPetApp.ViewModels
{
    public class TabMenuViewModel:BaseViewModel
    {
        public ReactiveCommand<Unit,Unit> SelectSpecialityCommand { get; set; }
        public TabMenuViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
            SelectSpecialityCommand = ReactiveCommand.CreateFromTask<Unit>((param) => 
                NavigationService.NavigateAsync(ConstantUri.SelectSpeciality,null,true));
        }
    }
}