using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData.Binding;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace NotiPetApp.ViewModels
{
    public class VaccinesViewModel:BaseViewModel,IInitialize
    {
        private readonly IPetsService _petsService;
        public Pet Pet { get; set; }
        public Vaccinate CurrentVaccine { get; set; }

        private readonly  ReadOnlyObservableCollection<Vaccinate> _vaccinates;
        public ObservableCollection<Vaccinate> Vaccinates { get; set; }
        
        
        public VaccinesViewModel(INavigationService navigationService, IPageDialogService dialogPage,IPetsService petsService) : base(navigationService, dialogPage)
        {
            _petsService = petsService;
        }

        Task ShowPdf()
        {
           return NavigationService.NavigateAsync(ConstantUri.Pdf,new NavigationParameters()
           {
               {ParameterConstant.Vacinnes,CurrentVaccine}
           });
        }
        protected override IObservable<Unit> ExecuteInitialize()
        {
            return _petsService.GetVaccinesByPet(Pet.Id)
                .Select(x=>Unit.Default);
        }

        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(ParameterConstant.Vacinnes))
            {
                Pet = parameters[ParameterConstant.Vacinnes] as Pet;
            }
        }
    }
}