using System.Linq;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.Reactive.Testing;
using NotiPet.Domain.Models;
using NotiPet.Domain.Validator;
using NotiPet.UnitTest.Fixtures;
using NotiPet.UnitTest.Fixtures.User;
using NotiPetApp.ViewModels;
using Xunit;

namespace NotiPet.UnitTest.ValidatorTest
{
    public class AuthenticationViewModelValidatorTester
    {
        private readonly AuthenticationValidator _validation = new AuthenticationValidator();
        [Fact]
        public void Should_have_error_when_InvalidEmail()
        {     
            var model = new LoginViewModelFixture();
            //arrange
            var authentication = (LoginViewModel)model;
            authentication.Email = "hello.com";
            //act
           var result = _validation.TestValidate(authentication);
           //asset
           result.ShouldHaveValidationErrorFor(e => e.Email);
        }
        [Fact]
        public void Should_have_error_when_PasswordEmptyOrNull()
        {
            var model = new LoginViewModelFixture();
            //arrange
            var authentication = (LoginViewModel)model;
            authentication.Email = "fiji1984@eiibps.com";
            
            //act
            var result = _validation.TestValidate(authentication);
            //asset
            result.ShouldHaveValidationErrorFor(e=>e.Password);
        }
        [Fact]
        public void Should_have_Message_error_when_PasswordOrUsernameEmptyOrNull()
        {
            var model = new LoginViewModelFixture();
            //arrange
            var authentication = (LoginViewModel)model;
            //act
            var result = _validation.TestValidate(authentication);
            //asset
            result.ShouldHaveValidationErrorFor(e => e);
        }
    }
}