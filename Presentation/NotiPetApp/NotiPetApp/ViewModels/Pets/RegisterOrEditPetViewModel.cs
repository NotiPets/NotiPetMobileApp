using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Bogus.DataSets;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Controls;
using NotiPetApp.Helpers;
using NotiPetApp.Models;
using NotiPetApp.Services;
using NotiPetApp.Views.Vets;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Syncfusion.XForms.Buttons;
using Xamarin.CommunityToolkit.Effects;

namespace NotiPetApp.ViewModels
{

    public class RegisterOrEditPetViewModel : BaseViewModel
    {
        private readonly IPetsService _petsService;
        private readonly IDeviceUtils _deviceUtils;
        private int _selectedIndex;
        public List<string> Gender { get; set; }
        
        public List<PetInformation> InformationAditional { get; set; }
        public List<PetType> PetsTypes { get; set; }

        public List<PetSize> PetSizes { get; set; }
        [Reactive]public PetType SelectedPetType { get; set; }

       [Reactive] public CreatePetModel Pet { get; set; }
       [Reactive]public PetType SelectedPetSize { get; set; }
       [Reactive] public string PictureUrl { get; set; }
        
        public RegisterOrEditPetViewModel(INavigationService navigationService, IPageDialogService dialogPage,IPetsService petsService,IDeviceUtils deviceUtils) : base(navigationService, dialogPage)
        {
            _petsService = petsService;
            _deviceUtils = deviceUtils;
            Pet = new CreatePetModel();
            InformationAditional = _petsService.PetInformations;
            PetsTypes = _petsService.PetTypes;
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());
            SaveCommand = ReactiveCommand.CreateFromObservable(SavePet);
            var canExecute =  SaveCommand.Select(x => x != null);
            
            Gender = new List<string>(){
                "Female",  "Male" 
            };
            PetSizes = _petsService.PetSizes;
            SaveCommand
                .Select(x=>Unit.Default)
                .InvokeCommand(ReactiveCommand.CreateFromTask(()=> NavigationService.GoBackAsync(),canExecute));//canExecute
            GetPhotoCommand = ReactiveCommand.CreateFromTask(TakePhoto);
        }

        public ReactiveCommand<Unit, Unit> GetPhotoCommand { get; set; }

        async Task TakePhoto()
        {
            PictureUrl = await _deviceUtils.TakePhotoAsync();
        }
        public ReactiveCommand<Unit, Pet> SaveCommand { get; }

        IObservable<Pet> SavePet()
        {
            Pet.User = Settings.UserId;
            Pet.PictureUrl = PictureUrl;
            Pet.PetType = SelectedPetType?.Id;
            Pet.Size = SelectedPetSize?.Id;
            return _petsService.SavePet(Pet);
        }

        public ReactiveCommand<Unit,Unit> NavigateGoBackCommand { get; set; }
    }
}