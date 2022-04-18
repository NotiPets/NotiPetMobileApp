using System.Windows.Navigation;
using NotiPet.Domain.Service;
using NotiPet.UnitTest.ViewModelTest;
using NotiPetApp.Services;
using NotiPetApp.ViewModels.Activity;
using Prism.Services;

namespace NotiPet.UnitTest.Fixtures.ActivityTest
{
    public class SelectSpecialityViewModelFixture:BaseViewModelFixture,IBuilder
    {
        public static implicit operator SelectSpecialityViewModel(SelectSpecialityViewModelFixture viewModel) =>
            viewModel.Build();

        private SelectSpecialityViewModel Build() => new SelectSpecialityViewModel(NavigationService,PageDialogService,_schedulerProvider,_petsService );

        public SelectSpecialityViewModelFixture SpecialistsServiceWith(IPetsService petsService) =>
            this.With(ref _petsService, petsService);
        private IPetsService _petsService;
        public SelectSpecialityViewModelFixture SchedulerProviderWith(SchedulerProvider schedulerProvider) =>
            this.With(ref _schedulerProvider, schedulerProvider);
        private ISchedulerProvider _schedulerProvider;
    }
}