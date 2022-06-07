using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using NotiPetApp.Services;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NotiPetApp.ViewModels
{
    public class EditProfileViewModel : BaseViewModel,IInitialize,IEditUserRequest
    {
        private readonly IUserService _userService;
        private readonly IDeviceUtils _deviceUtils;
        private readonly IVeterinaryService _veterinaryService;
        public User User { get;   set; }

        public ReactiveCommand<Unit,User> UpdateCommand { get; set; }
        private  ObservableAsPropertyHelper<IEnumerable<Veterinary>> _veterinaries;
       [Reactive] public Veterinary Veterinary { get; set; }
        public IEnumerable<Veterinary>  Veterinaries => _veterinaries?.Value;
        public EditProfileViewModel(INavigationService navigationService, IPageDialogService dialogPage,
            IUserService userService,IDeviceUtils deviceUtils,IVeterinaryService veterinaryService) : base(navigationService, dialogPage)
        {
            _userService = userService;
            _deviceUtils = deviceUtils;
            _veterinaryService = veterinaryService;
            UpdateCommand = ReactiveCommand.CreateFromObservable(UpdateUser);
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());
            TakePhotoCommand =  ReactiveCommand.CreateFromTask(TakePhoto);
            UpdateCommand.WhereNotNull().InvokeCommand(NavigateGoBackCommand);
            this.WhenAnyValue(x => x.Veterinaries)
                .WhereNotNull()
                .Subscribe((next) =>
                {
                    Veterinary = next.FirstOrDefault(x => x.Id == BusinessId);
                }).DisposeWith(Subscriptions);
        }

        public ReactiveCommand<Unit, Unit> NavigateGoBackCommand { get; }
        public ReactiveCommand<Unit, Unit> TakePhotoCommand { get; }

        async Task TakePhoto()
        {
            PictureUrl = await _deviceUtils.TakePhotoAsync();
        }

         IObservable<User> UpdateUser()
         {
             User.SetToEditUserRequest(this);
             return  _userService.UpdateUser(User.Id, User);
        }
         protected override IObservable<Unit> ExecuteInitialize()
         {
             return Observable.Create<Unit>(observer =>
             {
                 var compositeDisposable = new CompositeDisposable();

                 var getVeterinary =  _veterinaryService.GetVeterinary();
                 _veterinaries = getVeterinary.ToProperty(this, x => x.Veterinaries);
                 getVeterinary.Select(x => Unit.Default)
                     .Subscribe(observer)
                     .DisposeWith(compositeDisposable);
                 
                 return compositeDisposable;
             });
         }
         public void Initialize(INavigationParameters parameters)
         {
             if (parameters.ContainsKey(ParameterConstant.User))
             {
                 User = (User) parameters[ParameterConstant.User];
                 User.ConvertToEditUserRequest(this);
             }
         }

         [Reactive]  public string Names { get; set; }
         [Reactive] public string Username { get; set; }
         [Reactive]  public string Lastnames { get; set; }
        [Reactive]  public string Email { get; set; }
        [Reactive] public string Phone { get; set; }
        [Reactive]  public int BusinessId { get; set; }
        [Reactive] public string PictureUrl { get; set; }
    }
}