namespace AgileTracker.Client.Startup.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net.Http;
    using System.Threading.Tasks;

    using IdentityModel.Client;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.IdentityModel.Protocols.OpenIdConnect;

    public class RefreshExpiredTokensMiddleware
    {
        private RequestDelegate next;
        private readonly IHttpClientFactory clientFactory;
        private string authority;
        private string clientId;
        private string clientSecret;
        private string cookieSchemeName;

        public RefreshExpiredTokensMiddleware(
            RequestDelegate next,
            IHttpClientFactory clientFactory,
            string authority,
            string clientId,
            string clientSecret,
            string cookieSchemeName)
        {
            this.next = next;
            this.clientFactory = clientFactory;
            this.authority = authority;
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.cookieSchemeName = cookieSchemeName;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string oldAccessToken = await httpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            if (!string.IsNullOrEmpty(oldAccessToken))
            {
                JwtSecurityToken tokenInfo = new JwtSecurityToken(oldAccessToken);

                // Renew access token if pass halfway of its lifetime
                if (tokenInfo.ValidFrom + (tokenInfo.ValidTo - tokenInfo.ValidFrom) / 2 < DateTime.UtcNow)
                {
                    var serverClient = this.clientFactory.CreateClient();
                    var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync(authority);

                    string oldRefreshToken = await httpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
                    var refreshTokenResponse = await serverClient.RequestRefreshTokenAsync(new RefreshTokenRequest
                    {
                        Address = discoveryDocument.TokenEndpoint,
                        ClientId = this.clientId,
                        ClientSecret = this.clientSecret,
                        RefreshToken = oldRefreshToken
                    });

                    if (!refreshTokenResponse.IsError)
                    {
                        string idToken = await httpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
                        string newAccessToken = refreshTokenResponse.AccessToken;
                        string newRefreshToken = refreshTokenResponse.RefreshToken;

                        var tokens = new List<AuthenticationToken>
                    {
                        new AuthenticationToken { Name = OpenIdConnectParameterNames.IdToken, Value = idToken },
                        new AuthenticationToken { Name = OpenIdConnectParameterNames.AccessToken, Value = newAccessToken },
                        new AuthenticationToken { Name = OpenIdConnectParameterNames.RefreshToken, Value = newRefreshToken }
                    };

                        AuthenticateResult info = await httpContext.AuthenticateAsync(cookieSchemeName);

                        info.Properties.StoreTokens(tokens);

                        await httpContext.SignInAsync(cookieSchemeName, info.Principal, info.Properties);
                    }
                }
            }

            await next.Invoke(httpContext);
        }
    }
}
