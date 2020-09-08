namespace AgileTracker.Gateway.Authentication.Startup.Features.Identity.Register
{
    public class RegisterViewModel
    {
        public string Firstname { get; set; } = default!;

        public string Lastname { get; set; } = default!;

        public string EmailAddress { get; set; } = default!;

        public string Password { get; set; } = default!;

        public string ConfirmPassword { get; set; } = default!;

        public string ReturnUrl { get; set; } = default!;
    }
}
