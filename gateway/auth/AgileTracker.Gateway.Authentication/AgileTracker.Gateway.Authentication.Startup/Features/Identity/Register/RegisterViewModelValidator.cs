namespace AgileTracker.Gateway.Authentication.Startup.Features.Identity.Register
{
    using FluentValidation;

    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            this.RuleFor(u => u.EmailAddress)
                .NotEmpty()
                .WithMessage("Email address is required");

            this.RuleFor(u => u.EmailAddress)
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("'{PropertyValue}' is not a valid email address");

            this.RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("Password is required");

            this.RuleFor(u => u.Password)
                .MinimumLength(4)
                .WithMessage("Password must be atleast 4 symbols long");

            this.RuleFor(u => u.ConfirmPassword)
                .Must((u, s) => u.Password == s)
                .WithMessage("Passwords do not match");

            this.RuleFor(u => u.Firstname)
                .NotEmpty()
                .WithMessage("First name is required");

            this.RuleFor(u => u.Lastname)
                .NotEmpty()
                .WithMessage("Last name is required");
        }
    }
}
