using System.Windows.Navigation;
using NotiPet.Domain.Service;
using NotiPet.UnitTest.ViewModelTest;
using NotiPetApp.Services;
using NotiPetApp.ViewModels;
using Prism.Services;

namespace NotiPet.UnitTest.Fixtures.VeterinaryTest
{
    public class VetTabViewModelFixture:BaseViewModelFixture,IBuilder
    {
        public static implicit operator VetTabViewModel(VetTabViewModelFixture veterinaryViewModelFixture)=>veterinaryViewModelFixture.Build();

        private VetTabViewModel Build() => new VetTabViewModel(NavigationService,PageDialogService,_veterinaryService,_schedulerProvider,_specialistsService);

 
        public VetTabViewModelFixture SchedulerProviderWith(ISchedulerProvider schedulerProvider) =>
            this.With(ref _schedulerProvider, schedulerProvider);
        public VetTabViewModelFixture VeterinaryServiceWith(IVeterinaryService veterinaryService) =>
            this.With(ref _veterinaryService, veterinaryService);
        public VetTabViewModelFixture SpecialistsServiceWith(ISpecialistsService specialistsService) =>
            this.With(ref _specialistsService, specialistsService);
        private IVeterinaryService _veterinaryService;
        private ISchedulerProvider _schedulerProvider;
        private ISpecialistsService _specialistsService;


    }
}