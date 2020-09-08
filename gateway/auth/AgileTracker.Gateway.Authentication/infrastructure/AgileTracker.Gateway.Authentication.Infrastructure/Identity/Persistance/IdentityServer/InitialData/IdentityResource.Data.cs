namespace AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.IdentityServer.InitialData
{
    using System;
    using System.Collections.Generic;

    using IdentityServer4.EntityFramework.Entities;
    using IdentityServer4.EntityFramework.Mappers;

    public class IdentityResourceData : IIdentityServerInitialData
    {
        public Type EntityType => typeof(IdentityResource);

        public IEnumerable<object> GetData()
            => new List<IdentityResource>
            {
                new IdentityServer4.Models.IdentityResources.OpenId().ToEntity(),
                new IdentityServer4.Models.IdentityResources.Profile().ToEntity(),
            };
    }
}
