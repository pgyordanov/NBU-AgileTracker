namespace AgileTracker.Gateway.Authentication.Startup.Features.Identity.Login
{
    public class LoginViewModel
    {
        public string EmailAddress { get; set; } = default!;

        public string Password { get; set; } = default!;

        public bool RememberMe { get; set; } = default!;

        public string ReturnUrl { get; set; } = default!;
    }
}
