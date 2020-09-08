namespace AgileTracker.Gateway.Authentication.Startup.Features.Identity.Login
{
    using FluentValidation;

    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            this.RuleFor(u => u.EmailAddress)
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("'{PropertyValue}' is not a valid email address");
        }
    }
}
