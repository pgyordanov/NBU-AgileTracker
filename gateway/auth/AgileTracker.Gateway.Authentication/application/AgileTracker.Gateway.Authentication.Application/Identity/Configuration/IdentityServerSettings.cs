namespace AgileTracker.Gateway.Authentication.Application.Identity.Configuration
{
    public class IdentityServerSettings
    {
        public string IdentityIssuer { get; private set; } = default!;

        public string WebClientRedirectUrl { get; private set; } = default!;

        public string WebClientPostLogourRedirectUrl { get; private set; } = default!;
    }
}
