using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using NotiPetApp.Properties;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace NotiPetApp.ViewModels
{
    public class SpecialistDetailViewModel:BaseViewModel,IInitialize
    {
        private readonly ISpecialistsService _specialistsService;
        public ReactiveCommand<string,Specialist> GetSpecialistIdCommand { get; set; }
        public ObservableAsPropertyHelper<Specialist> _specialist { get; set; }
        public Specialist Specialist => _specialist?.Value;
        public SpecialistDetailViewModel(INavigationService navigationService, IPageDialogService dialogPage,ISpecialistsService specialistsService) : base(navigationService, dialogPage)
        {
            _specialistsService = specialistsService;
            GetSpecialistIdCommand = ReactiveCommand.CreateFromObservable<string,Specialist>(GetSpecialistById);
            InitializeCommand.InvokeCommand(GetSpecialistIdCommand);
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());
        }

        public ReactiveCommand<Unit, Unit> NavigateGoBackCommand { get;}

        public IObservable<Specialist> GetSpecialistById(string id)
        {
            return _specialistsService.GetSpecialistById(id);
        }
        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(ParameterConstant.SpecialistId))
            {
                _specialist =    GetSpecialistIdCommand.Execute(parameters[ParameterConstant.SpecialistId].ToString())
                    .ToProperty(this, x => x.Specialist);;
            }
         }
     }
}