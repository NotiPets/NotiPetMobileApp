
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using NotiPetApp.Models;
using NotiPetApp.Properties;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NotiPetApp.ViewModels
{
    public class ProfileViewModel:BaseViewModel
    {
        private readonly IUserService _userService;
        private ObservableAsPropertyHelper<User> _user;
        public User User => _user?.Value;
        public ReactiveCommand<Unit,Unit> NavigateToEditProfileCommand{ get; set; }
        public ReactiveCommand<Unit,Unit> NavigateToHelpPageCommand{ get; set; }

        //NavigateToEditProfileCommand
        public ProfileViewModel(INavigationService navigationService, IPageDialogService dialogPage,
            IUserService userService) : base(navigationService, dialogPage)
        {
            _userService = userService;
            AppMenuItems = new ObservableCollection<AppMenuItem>()
            {
                new(AppResources.MyPets,"patas",1,ReactiveCommand.CreateFromTask(NavigateToMyPets),SizeItem.Small),
                new(AppResources.AboutPage,"question",2,ReactiveCommand.CreateFromTask(NavigateToAbout),SizeItem.Small),
                new(AppResources.Settings,"settings",2,ReactiveCommand.CreateFromTask(NavigateToSettings),SizeItem.Small),
                new(AppResources.Logout,"logOut",4,ReactiveCommand.CreateFromTask(LogOut),SizeItem.Small),
            };
            NavigateToHelpPageCommand= ReactiveCommand.CreateFromTask<Unit>((b,token)=> NavigationService.NavigateAsync("HelpPage",parameters:new NavigationParameters()
            {
                {ParameterConstant.User,User}
            },true)); 
            NavigateToEditProfileCommand = ReactiveCommand.CreateFromTask<Unit>((param) =>
            {
                return NavigationService.NavigateAsync(ConstantUri.EditProfile);
            });
            NavigateToEditUserCommand = ReactiveCommand.CreateFromTask<Unit>((b,token)=> NavigationService.NavigateAsync(ConstantUri.UserEdit,parameters:new NavigationParameters()
            {
                {ParameterConstant.User,User}
            }));
        }

        protected override IObservable<Unit> ExecuteInitialize() => Observable.Create<Unit>(observable =>
        {
            var disposable = new CompositeDisposable();
            var getUser =  GetUserById();
            _user = getUser.ToProperty(this, e => e.User);
            getUser.Select(e => Unit.Default)
                .Subscribe(observable)
                .DisposeWith(disposable);
                return disposable;
        });

        public ReactiveCommand<Unit, Unit> NavigateToEditUserCommand { get; set; }

        Task NavigateToMyPets()
        {
            return NavigationService.NavigateAsync(ConstantUri.MyPets);
        }
        Task NavigateToAbout()
        {
            return NavigationService.NavigateAsync(ConstantUri.About);
        }
        Task NavigateToSettings()
        {
            return NavigationService.NavigateAsync(ConstantUri.Settings);
        }
       public ObservableCollection<AppMenuItem> AppMenuItems { get; }

       IObservable<User> GetUserById()
       {
           return _userService.GetUserById(Settings.Username);
       }
       Task LogOut()
       {
           Settings.ClearStorage();
             return   NavigationService.NavigateAsync(ConstantUri.Login);
        }
    }
}