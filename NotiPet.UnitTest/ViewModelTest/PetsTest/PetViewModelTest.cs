using System;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using Microsoft.Reactive.Testing;
using NotiPet.Data.Services;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPet.Mocks.Services;
using NotiPet.UnitTest.Fixtures.PetTest;
using NotiPet.UnitTest.Services;
using NotiPetApp.Services;
using NotiPetApp.ViewModels;
using ReactiveUI.Testing;
using Xunit;

namespace NotiPet.UnitTest.ViewModelTest
{
    public class PetViewModelTest:BaseServiceTest
    {
        [Fact]
        public void PetsListNotBeEmptyWhenExecutingInitializing()
        {
            var scheduler = new TestScheduler(); 
                 
                var petServiceApi = new PetServiceApiMock();
                IPetsService petsService = new PetsService(petServiceApi,Mapper);
                var viewModelFixture = new PetsViewModelFixture()
                    .PetServiceWith(petsService)
                    .SchedulerWith(new SchedulerProvider(scheduler,scheduler));
    
                //Arrange
                var viewModel =(PetsViewModel) viewModelFixture;
            
                //Act
                viewModel.InitializingCommand.Execute().Subscribe();
                scheduler.AdvanceByMs(100);
                //Asset
                viewModel.Pets.Should().NotBeEmpty();

        }
        [Fact]
        public void SearchingPetShouldBeReturnResultBySearching()
        {
           var scheduler = new TestScheduler(); 
                var petServiceApi = new PetServiceApiMock();
                IPetsService petsService = new PetsService(petServiceApi, Mapper);
                var viewModelFixture = new PetsViewModelFixture()
                    .PetServiceWith(petsService)
                    .SchedulerWith(new SchedulerProvider(scheduler, scheduler));
                
                //Arrange
                var viewModel = (PetsViewModel) viewModelFixture;
                var text = "Labrador Retriever";
                Expression<Func<Pet, bool>> expression = e => petsService.SearchPredicate(text).Invoke(e);
                //Act
                viewModel.InitializingCommand.Execute().Subscribe();
                viewModel.SearchText = text;
                scheduler.AdvanceByMs(100);

        }
    }
}