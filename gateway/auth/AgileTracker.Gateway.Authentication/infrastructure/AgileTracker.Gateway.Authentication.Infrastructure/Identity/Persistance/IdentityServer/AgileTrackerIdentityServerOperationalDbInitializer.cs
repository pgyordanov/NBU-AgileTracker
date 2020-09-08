namespace AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.IdentityServer
{
    using System.Collections.Generic;

    using AgileTracker.Common.Infrastructure;
    using AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.IdentityServer.InitialData;

    using IdentityServer4.EntityFramework.DbContexts;

    using Microsoft.EntityFrameworkCore;

    internal class AgileTrackerIdentityServerOperationalDbInitializer : IInitializer
    {
        private readonly PersistedGrantDbContext db;

        public AgileTrackerIdentityServerOperationalDbInitializer(PersistedGrantDbContext db)
        {
            this.db = db;
        }

        public void Initialize()
        {
            this.db.Database.Migrate();
        }
    }
}
