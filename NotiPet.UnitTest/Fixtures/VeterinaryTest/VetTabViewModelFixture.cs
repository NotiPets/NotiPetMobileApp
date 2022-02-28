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

        private VetTabViewModel Build() => new VetTabViewModel(NavigationService,PageDialogService);


    }
}