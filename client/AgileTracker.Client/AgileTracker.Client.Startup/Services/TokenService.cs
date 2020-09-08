namespace AgileTracker.Client.Startup.Services
{
    using AgileTracker.Client.Infrastructure.Contracts;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;

    public class TokenService : ITokenService
    {
        public TokenService(IHttpContextAccessor httpContextAccessor)
        {
            var rawIdToken = httpContextAccessor.HttpContext.GetTokenAsync("id_token").GetAwaiter().GetResult();
            var rawAccessToken = httpContextAccessor.HttpContext.GetTokenAsync("access_token").GetAwaiter().GetResult();

            this.IdToken = rawIdToken;
            this.AccessToken = rawAccessToken;
        }

        public string IdToken { get; }

        public string AccessToken { get; }
    }
}
