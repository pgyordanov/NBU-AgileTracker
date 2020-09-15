namespace AgileTracker.StatisticsService.Infrastructure.Persistance
{
    using System.Reflection;

    using AgileTracker.StatisticsService.Domain.Models.TaskEstimation;
    using AgileTracker.StatisticsService.Infrastructure.Persistance.Models;

    using Microsoft.EntityFrameworkCore;

    public class AgileTrackerStatisticsDbContext : DbContext
    {
        public AgileTrackerStatisticsDbContext(DbContextOptions<AgileTrackerStatisticsDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskEstimation> TaskEstimations { get; set; } = default!;

        public DbSet<ProjectGroupOwnership> ProjectGroupOwnerships { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
