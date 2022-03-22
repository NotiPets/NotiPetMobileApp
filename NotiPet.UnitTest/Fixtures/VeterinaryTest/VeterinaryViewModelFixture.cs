using NotiPet.Domain.Service;
using NotiPet.UnitTest.ViewModelTest;
using NotiPetApp.Services;
using NotiPetApp.ViewModels;

namespace NotiPet.UnitTest.Fixtures.VeterinaryTest
{
    public class VeterinaryViewModelFixture: BaseViewModelFixture, IBuilder
    {
        public static implicit operator VeterinaryViewModel(VeterinaryViewModelFixture specialist) =>
            specialist.Build();

        private VeterinaryViewModel Build() => new VeterinaryViewModel(NavigationService, PageDialogService,
            _veterinaryService, _schedulerProvider);


        public VeterinaryViewModelFixture SchedulerProviderWith(ISchedulerProvider schedulerProvider) =>
            this.With(ref _schedulerProvider, schedulerProvider);

        public VeterinaryViewModelFixture VeterinaryServiceWith(IVeterinaryService veterinaryService) =>
            this.With(ref _veterinaryService, veterinaryService);

        private ISchedulerProvider _schedulerProvider;
        private IVeterinaryService _veterinaryService;


    }
}