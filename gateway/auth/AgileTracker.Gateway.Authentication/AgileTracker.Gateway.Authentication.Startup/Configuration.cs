using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace AgileTracker.Gateway.Authentication.IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "api1"
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope
                {
                    Name = "idp"
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                 new Client
                {
                    ClientId = "idp",
                    ClientSecrets = { new Secret("ipd_secret".ToSha256()) },

                    // No interactive user, uses the ClientId & Secret for authentication.
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // Scopes that this client has access to.
                    AllowedScopes = { "idp" }
                }
            };
        }
    }
}
