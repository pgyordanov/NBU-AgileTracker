﻿namespace AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.IdentityServer.InitialData
{
    using System;
    using System.Collections.Generic;

    using IdentityServer4;
    using IdentityServer4.EntityFramework.Mappers;

    public class ApiResourceData : IIdentityServerInitialData
    {
        public Type EntityType => typeof(IdentityServer4.EntityFramework.Entities.ApiResource);

        public IEnumerable<object> GetData()
            => new List<IdentityServer4.EntityFramework.Entities.ApiResource>
            {
                new IdentityServer4.Models.ApiResource(IdentityServerConstants.LocalApi.ScopeName).ToEntity(),
                new IdentityServer4.Models.ApiResource("AgileTrackerGateway").ToEntity()
            };
    }
}
