namespace AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.Identity
{
    using System.Reflection;

    using AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.Identity.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    internal class AgileTrackerIdentityDbContext: IdentityDbContext<AgileTrackerUser>
    {
        public AgileTrackerIdentityDbContext(DbContextOptions<AgileTrackerIdentityDbContext> dbContextOptions)
            :base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
