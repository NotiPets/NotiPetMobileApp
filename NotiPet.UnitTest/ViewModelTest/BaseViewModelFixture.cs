using AutoMapper;
using Bogus;
using FakeItEasy;
using NotiPet.Data.Mappers;
using Prism.Navigation;
using Prism.Services;

namespace NotiPet.UnitTest.ViewModelTest
{
    public class BaseViewModelFixture
    {
        protected readonly INavigationService NavigationService;
        protected readonly IPageDialogService PageDialogService;
        protected readonly IMapper  Mapper;
        public BaseViewModelFixture()
        {
            Mapper = new Mapper(AutoMapperConfig.GetConfig());
            NavigationService = A.Fake<INavigationService>();
            PageDialogService = A.Fake<IPageDialogService>();
        }
    }
}