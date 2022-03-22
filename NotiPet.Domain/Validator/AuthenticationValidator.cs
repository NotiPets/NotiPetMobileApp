using FluentValidation;
using NotiPet.Domain.Models;

namespace NotiPet.Domain.Validator
{
    public class AuthenticationValidator:AbstractValidator<IAuthenticationRequestViewModel>
    {
        public AuthenticationValidator()
        {
            this.RuleFor(e => e.Password)
                .Must(e=>!string.IsNullOrWhiteSpace(e))
                .WithMessage("Password not valid");
            this.RuleFor(e => e.Username)
                .Must(e=>!string.IsNullOrWhiteSpace(e))
                .WithMessage("Username not valid");
            this.RuleFor(x => x)
                .Must(e => 
                    !string.IsNullOrEmpty(e.Password) &&
                    !string.IsNullOrEmpty(e.Username))
                .WithMessage("has fields empty!");;
        }
    }
    public class RegisterValidator:AbstractValidator<IRegisterRequestViewModel>
    {
        public RegisterValidator()
        {
            this.RuleFor(e => e.Email)
                .Must(e=>!string.IsNullOrWhiteSpace(e))
                .EmailAddress()
                .WithMessage("Email not valid");
            this.RuleFor(e => e.Password)
                .Must(e=>!string.IsNullOrWhiteSpace(e))
                .WithMessage("Password not valid");
            this.RuleFor(e => e.Username)
                .Must(e=>!string.IsNullOrWhiteSpace(e))
                .WithMessage("Username not valid");
            this.RuleFor(e => e.Address1)
                .Must(e=>!string.IsNullOrWhiteSpace(e))
                .WithMessage("Address1 not valid");
            this.RuleFor(e => e.City)
                .Must(e=>!string.IsNullOrWhiteSpace(e))
                .WithMessage("City not valid");
            this.RuleFor(e => e.Phone)
                .Must(e=>!string.IsNullOrWhiteSpace(e))
                .WithMessage("Phone not valid");
            this.RuleFor(e => e.Province)
                .Must(e=>!string.IsNullOrWhiteSpace(e))
                .WithMessage("Province not valid");
            this.RuleFor(e => e.Document)
                .NotNull()
                .WithMessage("PersonalDocument not valid");
            this.RuleFor(e => e.LastName)
                .Must(e=>!string.IsNullOrWhiteSpace(e))
                .WithMessage("LastName not valid");
            this.RuleFor(e => e.Name)
                .Must(e=>!string.IsNullOrWhiteSpace(e))
                .WithMessage("Name not valid");
            this.RuleFor(e => e)
                .Must(e=>!string.IsNullOrWhiteSpace(e.ConfirmPassword)&&e.ConfirmPassword.Equals(e.Password))
                .WithMessage("ConfirmPassword isn't equal");

        }
    }
}