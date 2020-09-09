namespace AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.IdentityServer.InitialData
{
    using System;
    using System.Collections.Generic;

    using IdentityModel;

    using IdentityServer4;
    using IdentityServer4.EntityFramework.Entities;
    using IdentityServer4.EntityFramework.Mappers;

    public class ClientData : IIdentityServerInitialData
    {
        public Type EntityType => typeof(Client);

        public IEnumerable<object> GetData()
            => new List<Client>
            {
                new IdentityServer4.Models.Client
                {
                    ClientName = "agiletracker_web",
                    ClientId = "agiletracker_web",
                    ClientSecrets = 
                    {
                        new IdentityServer4.Models.Secret("agiletracker_web".ToSha256())
                    },

                    AllowedGrantTypes = IdentityServer4.Models.GrantTypes.Code,

                    // default URLs
                    RedirectUris = { "https://localhost:44397/signin-oidc"},
                    PostLogoutRedirectUris = { "https://localhost:44397/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        $"{IdentityServerConstants.LocalApi.ScopeName}",
                        "AgileTrackerGateway",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess
                    },

                    //Two round-trips to get the user claims from the userinfo endpoint
                    //AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true,

                    RequireConsent = false
                }
                .ToEntity()
            };
    }
}
