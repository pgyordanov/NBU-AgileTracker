namespace AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.IdentityServer.InitialData
{
    using System;
    using System.Collections.Generic;

    using IdentityServer4.EntityFramework.Entities;
    using IdentityServer4.EntityFramework.Mappers;

    public class ApiScopeData : IIdentityServerInitialData
    {
        public Type EntityType => typeof(ApiScope);

        public IEnumerable<object> GetData()
            => new List<ApiScope>
            {
                new IdentityServer4.Models.ApiScope($"{IdentityServer4.IdentityServerConstants.LocalApi.ScopeName}.access")
                    .ToEntity(),
                new IdentityServer4.Models.ApiScope("AgileTrackerGateway.access").ToEntity()
            };
    }
}
