using System;
using FluentAssertions;
using Microsoft.Reactive.Testing;
using NotiPet.Data.Services;
using NotiPet.Domain.Models;
using NotiPet.Mocks.Services;
using NotiPet.UnitTest.Fixtures.VeterinaryTest;
using NotiPetApp.Services;
using NotiPetApp.ViewModels;
using ReactiveUI.Testing;
using Xunit;

namespace NotiPet.UnitTest.ViewModelTest.VetTest
{
    public class SpecialistViewModelTest:BaseViewModelFixture
    {
        [Fact]
        public void ShouldBeLoadSpecialistCollectionsWhenInitialize()
        {
            var scheduler = new TestScheduler();
            var mainFixture = new SpecialistViewModelFixture()
                .SchedulerProviderWith(new SchedulerProvider(scheduler, scheduler))
                .SpecialistsServiceWith(new SpecialistsService(Mapper, new SpecialistsServiceApiMock()));
            SpecialistViewModel viewModel = mainFixture;
            viewModel.InitializeCommand.Execute().Subscribe();
            scheduler.AdvanceByMs(500);
            scheduler.AdvanceByMs(200);
            viewModel.Specialists.Should().NotBeEmpty();
        }
    }
}