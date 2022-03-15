using System;
using System.Linq.Expressions;
using AutoMapper;
using FluentAssertions;
using Microsoft.Reactive.Testing;
using NotiPet.Data.Services;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPet.Mocks.Services;
using NotiPet.UnitTest.Fixtures.PetTest;
using NotiPet.UnitTest.Fixtures.VeterinaryTest;
using NotiPet.UnitTest.Services;
using NotiPetApp.Services;
using NotiPetApp.ViewModels;
using ReactiveUI.Testing;
using Xunit;

namespace NotiPet.UnitTest.ViewModelTest.VetTest
{
    public class VeterinaryViewModelTest:BaseServiceTest
    {
        [Fact]
        public void VetsListShouldNotBeEmptyWhenExecutingInitializing()
        {
            var scheduler = new TestScheduler();
            
            IBusinessServiceApi businessServiceApi = new BusinessServiceApiMock();
            IVeterinaryService veterinaryService = new VeterinaryService(businessServiceApi,Mapper);
            var viewModelFixture = new VeterinaryViewModelFixture()
                .VeterinaryServiceWith(veterinaryService)
                .SchedulerProviderWith(new SchedulerProvider(scheduler,scheduler));
    
            //Arrange
            var viewModel =(VeterinaryViewModel) viewModelFixture;
            
            //Act
            viewModel.InitializingCommand.Execute().Subscribe();
            scheduler.AdvanceByMs(100);
            //Asset
            viewModel.Veterinaries.Should().NotBeEmpty();
        }

        [Fact]
        public void SearchingPetShouldBeReturnResultBySearching()
        {
            var scheduler = new TestScheduler();

                IBusinessServiceApi businessServiceApi = new BusinessServiceApiMock();
                IVeterinaryService veterinaryService = new VeterinaryService(businessServiceApi,Mapper);
                var viewModelFixture = new VeterinaryViewModelFixture()
                    .VeterinaryServiceWith(veterinaryService)
                    .SchedulerProviderWith(new SchedulerProvider(scheduler,scheduler));
    
                //Arrange
                var viewModel =(VeterinaryViewModel) viewModelFixture;
                var text = "A";
                Expression<Func<Veterinary,bool>> predicate = e=> veterinaryService.SearchPredicate(text).Invoke(e);
                //Act
                viewModel.SearchText = text;
                viewModel.InitializingCommand.Execute().Subscribe();
                scheduler.AdvanceByMs(100);
                //Asset
                viewModel.Veterinaries.Should().OnlyContain(predicate);
             
        }
    }
}