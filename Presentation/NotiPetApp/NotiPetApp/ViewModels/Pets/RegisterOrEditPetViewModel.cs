using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Bogus.DataSets;
using FluentValidation;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPet.Domain.Validator;
using NotiPetApp.Controls;
using NotiPetApp.Helpers;
using NotiPetApp.Models;
using NotiPetApp.Services;
using NotiPetApp.Views.Vets;
using Prism.Common;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Syncfusion.XForms.Buttons;
using Xamarin.CommunityToolkit.Effects;

namespace NotiPetApp.ViewModels
{

    public class RegisterOrEditPetViewModel : BaseViewModel,IInitialize
    {
        private readonly IPetsService _petsService;
        private readonly IDeviceUtils _deviceUtils;
        private readonly CreatePetModelValidate _validate;
        private int _selectedIndex;
        public List<string> Gender { get; set; }
        
        public List<PetInformation> InformationAditional { get; set; }
        public List<PetType> PetsTypes { get; set; }

        public List<PetSize> PetSizes { get; set; }
        [Reactive]public PetType SelectedPetType { get; set; }

       [Reactive] public CreatePetModel Pet { get; set; }
       [Reactive]public PetSize SelectedPetSize { get; set; }
       [Reactive] public string PictureUrl { get; set; }
       public bool IsNew { get; set; } = true;
        
        public RegisterOrEditPetViewModel(INavigationService navigationService, IPageDialogService dialogPage,IPetsService petsService,IDeviceUtils deviceUtils,CreatePetModelValidate validate) : base(navigationService, dialogPage)
        {
            _petsService = petsService;
            _deviceUtils = deviceUtils;
            _validate = validate;
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
            var validate = _validate.Validate(Pet);
            if (!validate.IsValid)
            {
                ShowErrorMessage(validate.Errors.Select(e => e.ErrorMessage));
                return Observable.Return<Pet>(null);
            }
            return IsNew? _petsService.SavePet(Pet):_petsService.EditPet(Pet);
        }

        public ReactiveCommand<Unit,Unit> NavigateGoBackCommand { get; set; }
        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey (ParameterConstant.Pet))
            {
                IsNew = false;
                Pet =  new CreatePetModel(parameters[ParameterConstant.Pet] as Pet);
                SelectedPetSize = PetSizes.FirstOrDefault(x => x.Id == Pet.Size);
                SelectedPetType = PetsTypes.FirstOrDefault(x=>x.Id==Pet.PetType);
                PictureUrl = Pet.PictureUrl;
            }
        }
    }
}