using System;
using FluentAssertions;
using Microsoft.Reactive.Testing;
using NotiPet.Data.Services;
using NotiPet.Mocks.Services;
using NotiPet.UnitTest.Fixtures.ActivityTest;
using NotiPetApp.Services;
using NotiPetApp.ViewModels.Activity;
using ReactiveUI.Testing;
using Xunit;

namespace NotiPet.UnitTest.ViewModelTest.ActivityTest
{
    public class SelectSpecialityViewModelTest:BaseViewModelFixture
    {
        [Fact]
        public void ShouldBeReturnSpecialitiesWhenExecuteInitialize()
        {
            var testScheduler = new TestScheduler();
            var viewModel = new SelectSpecialityViewModelFixture()
                .SpecialistsServiceWith(new PetsService(new PetServiceApiMock(),Mapper))
                .SchedulerProviderWith(new SchedulerProvider(testScheduler,testScheduler));
            SelectSpecialityViewModel selectSpecialityViewModel = viewModel;
            selectSpecialityViewModel
                .InitializeCommand
                .Execute()
                .Subscribe();
            testScheduler.AdvanceByMs(200);
          //  selectSpecialityViewModel.Specialities.Should().NotBeEmpty();
        }

    }
}