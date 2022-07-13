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
        public DigitalVaccine Vaccinate { get; set; }

        public VaccinatePdf Pdf => _pdf?.Value;
        private ObservableAsPropertyHelper<VaccinatePdf> _pdf;
        //NavigateGoBackCommand
        public ReactiveCommand<Unit, Unit> NavigateGoBackCommand { get; set; }
        public VaccinesViewModel(INavigationService navigationService, IPageDialogService dialogPage,IPetsService petsService) : base(navigationService, dialogPage)
        {
            _petsService = petsService;
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());
        }

        protected override IObservable<Unit> ExecuteInitialize()
        {
            var pdfGenerated = _petsService.GetVaccinePdf(Vaccinate.Id);
             _pdf = pdfGenerated.ToProperty(this, x => x.Pdf);
             return  pdfGenerated.Select(x=>Unit.Default);
        }

        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(ParameterConstant.Vacinnes))
            {
                Vaccinate = parameters[ParameterConstant.Vacinnes] as DigitalVaccine;
            }
        }
    }
}