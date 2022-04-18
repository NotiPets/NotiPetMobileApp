using System.Reactive;
using System.Reactive.Linq;
using NotiPetApp.Helpers;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace NotiPetApp.ViewModels
{
    public class TabMenuViewModel:BaseViewModel
    {
        public ReactiveCommand<Unit,Unit> SelectSpecialityCommand { get; set; }
        public ObservableAsPropertyHelper<bool> _hasToken;
        public bool HasToken => _hasToken.Value;
        public TabMenuViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
            SelectSpecialityCommand = ReactiveCommand.CreateFromTask<Unit>((param) => 
                NavigationService.NavigateAsync(ConstantUri.SelectSpeciality,null,true));
            var existAnyTokenCommand = ReactiveCommand.CreateFromObservable<bool>(()=>Settings.Token.Select(e=>!string.IsNullOrEmpty(e)));
            _hasToken = existAnyTokenCommand.Execute().ToProperty(this, x => x.HasToken);
            InitializeCommand.InvokeCommand(existAnyTokenCommand);
        }
    }
}