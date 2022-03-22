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
    public class VeterinaryViewModelTest:BaseViewModelFixture
    {
        [Fact]
        public void ShouldBeLoadSpecialistCollectionsWhenInitialize()
        {
            var scheduler = new TestScheduler();
            var mainFixture = new VeterinaryViewModelFixture()
                .SchedulerProviderWith(new SchedulerProvider(scheduler, scheduler))
                .VeterinaryServiceWith(new VeterinaryService(new BusinessServiceApiMock(), Mapper));
            VeterinaryViewModel viewModel = mainFixture;
            viewModel.InitializeCommand.Execute().Subscribe();
            scheduler.AdvanceByMs(500);
            viewModel.Veterinaries.Should().NotBeEmpty();

        }
    }
}