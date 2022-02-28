using NotiPet.Domain.Service;
using NotiPet.UnitTest.ViewModelTest;
using NotiPetApp.Services;
using NotiPetApp.ViewModels;

namespace NotiPet.UnitTest.Fixtures.PetTest
{
    public class PetsViewModelFixture:BaseViewModelFixture,IBuilder
    {
        public static implicit operator PetsViewModel(PetsViewModelFixture petsViewModelFixture) =>
            petsViewModelFixture.Build();

        private PetsViewModel Build() => new PetsViewModel(NavigationService, PageDialogService, _petsService,_schedulerProvider);
        public PetsViewModelFixture SchedulerWith(ISchedulerProvider  schedule)=>
            this.With(ref _schedulerProvider,schedule);
        private ISchedulerProvider _schedulerProvider;
        public PetsViewModelFixture PetServiceWith(IPetsService petsService)=>
            this.With(ref _petsService,petsService);
        private IPetsService _petsService;
        
        

    }
}