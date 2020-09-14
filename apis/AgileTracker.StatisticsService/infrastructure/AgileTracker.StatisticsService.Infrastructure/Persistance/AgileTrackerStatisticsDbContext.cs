namespace AgileTracker.StatisticsService.Infrastructure.Persistance
{
    using System.Reflection;

    using Microsoft.EntityFrameworkCore;

    public class AgileTrackerStatisticsDbContext: DbContext
    {
        public AgileTrackerStatisticsDbContext(DbContextOptions<AgileTrackerStatisticsDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
