using FluentValidation.TestHelper;
using NotiPet.Domain.Validator;
using NotiPet.UnitTest.Fixtures;
using NotiPetApp.ViewModels;
using Xunit;

namespace NotiPet.UnitTest.ValidatorTest
{
    public class RegisterValidatorTest
    {
        private readonly RegisterValidator _validation = new RegisterValidator();

        [Fact]
        public void Should_have_Message_error_when_ShouldHaveAnyValidationError()
        {
            var model = new RegisterViewModelFixture();
            //arrange
            var authentication = (RegisterViewModel)model;
            //act
            var result = _validation.TestValidate(authentication);
            //asset
            result.ShouldHaveAnyValidationError();
        }
    }
}