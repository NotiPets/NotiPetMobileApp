using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
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
    public class PetsViewModel:BaseViewModel,INavigatedAware
    {
        private readonly IPetsService _petsService;
        private readonly  ReadOnlyObservableCollection<Pet> _pets;
        public ReadOnlyObservableCollection<Pet> Pets => _pets;
       public ReactiveCommand<Unit,Unit> NavigateToRegisterPetCommand{ get; set; }
       [Reactive]public Pet SelectedItem { get; set; }
       


        public PetsViewModel(INavigationService navigationService, IPageDialogService dialogPage,
            IPetsService petsService,    ISchedulerProvider currentThread) : base(navigationService, dialogPage)
        {
            _petsService = petsService;
            _petsService.Pets.Connect()
                .ObserveOn(currentThread.CurrentThread)
                .Bind(out _pets)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());
            NavigateToRegisterPetCommand = ReactiveCommand.CreateFromTask<Unit>((param) => NavigationService.NavigateAsync(ConstantUri.RegisterOrEditPet));
            EditPetCommand = ReactiveCommand.CreateFromTask<Pet>((param) => NavigationService.NavigateAsync(ConstantUri.RegisterOrEditPet,new NavigationParameters()
            {
                {ParameterConstant.Pet,param}
            }));
            DeletePetCommand = ReactiveCommand.CreateFromObservable<string,Unit>(RemovePet);
            ActionsCommand = ReactiveCommand.CreateFromTask<Pet>(ActionsToDo);

        }

        private async Task ActionsToDo(Pet pet)
        {
            var options = await DialogPage.DisplayActionSheetAsync("Choose",null,null,new []
            {
                "Show Vacinnes",
                "Cancel"
            });
            switch (@options)
            {
                case  "Show Vacinnes":
                    await ShowVaccines(pet);
                    break;
            }
           
        }

        public ReactiveCommand<Pet, Unit> ActionsCommand { get; set; }

     

        public ReactiveCommand<Pet, Unit> EditPetCommand { get; set; }

        public ReactiveCommand<string, Unit> DeletePetCommand { get; set; }

        public ReactiveCommand<Unit, Unit> NavigateGoBackCommand { get; set; }


        IObservable<Unit> RemovePet(string id)
            => _petsService.RemovePet(id).Select(e => Unit.Default);
        protected override IObservable<Unit> ExecuteInitialize()
            => _petsService.GetPets(Settings.UserId).Select(e => Unit.Default);

        Task ShowVaccines(Pet pet)
        {
            return NavigationService.NavigateAsync(ConstantUri.ShowVaccines, new NavigationParameters()
            {
                {ParameterConstant.Vacinnes,pet}
            });   
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
     
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode()== NavigationMode.Back)
            {
                InitializeCommand
                    .Execute()
                    .Subscribe()
                    .DisposeWith(Subscriptions);
            }
        }
    }
}