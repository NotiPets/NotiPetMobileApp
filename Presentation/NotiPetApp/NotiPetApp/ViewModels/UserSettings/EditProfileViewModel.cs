using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace NotiPetApp.ViewModels
{
    public class EditProfileViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        public User User { get;  private set; }
        public List<string> Gender { get; set; }
        public ReactiveCommand<Unit,User> UpdateCommand { get; set; }

        public EditProfileViewModel(INavigationService navigationService, IPageDialogService dialogPage,
            IUserService userService) : base(navigationService, dialogPage)
        {
            _userService = userService;
            Gender = new List<string>(){
                "Macho" , "Hembra"
            };
            UpdateCommand = ReactiveCommand.CreateFromObservable(UpdateUser);
        }

        protected override IObservable<Unit> ExecuteInitialize()
        {

            return Observable.Create<Unit>((observer) =>
            {
                var compositeDisposable = new CompositeDisposable();
                _userService.GetUserById(Settings.Username)
                    .Subscribe((x) =>
                    {
                        User = x;
                    })
                    .DisposeWith(Subscriptions);
                return compositeDisposable;
            });
        }

         IObservable<User> UpdateUser()
        {
          return  _userService.UpdateUser(User.Id, User);
        }
    }
}