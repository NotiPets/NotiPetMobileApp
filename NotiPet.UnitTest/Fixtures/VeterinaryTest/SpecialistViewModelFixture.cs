using NotiPet.Domain.Service;
using NotiPet.UnitTest.ViewModelTest;
using NotiPetApp.Services;
using NotiPetApp.ViewModels;

namespace NotiPet.UnitTest.Fixtures.VeterinaryTest
{
    public class SpecialistViewModelFixture : BaseViewModelFixture, IBuilder
    {
        public static implicit operator SpecialistViewModel(SpecialistViewModelFixture specialist) =>
            specialist.Build();

        private SpecialistViewModel Build() => new SpecialistViewModel(NavigationService, PageDialogService,
            _specialistsService, _schedulerProvider);


        public SpecialistViewModelFixture SchedulerProviderWith(ISchedulerProvider schedulerProvider) =>
            this.With(ref _schedulerProvider, schedulerProvider);

        public SpecialistViewModelFixture SpecialistsServiceWith(ISpecialistsService specialistsService) =>
            this.With(ref _specialistsService, specialistsService);

        private ISchedulerProvider _schedulerProvider;
        private ISpecialistsService _specialistsService;


    }
}