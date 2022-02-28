using NotiPet.Domain.Service;
using NotiPet.UnitTest.ViewModelTest;
using NotiPetApp.Services;
using NotiPetApp.ViewModels;

namespace NotiPet.UnitTest.Fixtures.VeterinaryTest
{
    public class VeterinaryViewModelFixture:BaseViewModelFixture,IBuilder
    {
        public static implicit operator VeterinaryViewModel(VeterinaryViewModelFixture veterinaryViewModelFixture)=>veterinaryViewModelFixture.Build();

        private VeterinaryViewModel Build() => new VeterinaryViewModel(NavigationService,PageDialogService,_veterinaryService,_schedulerProvider);

        public VeterinaryViewModelFixture VeterinaryServiceWith(IVeterinaryService veterinaryService) =>
            this.With(ref _veterinaryService, veterinaryService);
        public VeterinaryViewModelFixture SchedulerProviderWith(ISchedulerProvider scheduler) =>
            this.With(ref _schedulerProvider, scheduler);
        private IVeterinaryService _veterinaryService;
        private ISchedulerProvider _schedulerProvider;
    }
}