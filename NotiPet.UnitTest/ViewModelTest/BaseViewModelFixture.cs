using Bogus;
using FakeItEasy;
using Prism.Navigation;
using Prism.Services;

namespace NotiPet.UnitTest.ViewModelTest
{
    public class BaseViewModelFixture
    {
        protected readonly INavigationService NavigationService;
        protected readonly IPageDialogService PageDialogService;
        public BaseViewModelFixture()
        {
            NavigationService = A.Fake<INavigationService>();
            PageDialogService = A.Fake<IPageDialogService>();
        }
    }
}