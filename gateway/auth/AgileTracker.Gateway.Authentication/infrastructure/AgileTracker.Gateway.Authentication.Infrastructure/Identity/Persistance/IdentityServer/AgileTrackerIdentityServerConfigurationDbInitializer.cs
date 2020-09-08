namespace AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.IdentityServer
{
    using System.Collections.Generic;

    using AgileTracker.Common.Infrastructure;
    using AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.IdentityServer.InitialData;

    using IdentityServer4.EntityFramework.DbContexts;

    internal class AgileTrackerIdentityServerConfigurationDbInitializer : DbSeeder, IInitializer
    {
        private readonly ConfigurationDbContext db;

        public AgileTrackerIdentityServerConfigurationDbInitializer(
            ConfigurationDbContext db, 
            IEnumerable<IIdentityServerInitialData> dataProviders)
                :base(db, dataProviders)
        {
            this.db = db;
        }
    }
}
