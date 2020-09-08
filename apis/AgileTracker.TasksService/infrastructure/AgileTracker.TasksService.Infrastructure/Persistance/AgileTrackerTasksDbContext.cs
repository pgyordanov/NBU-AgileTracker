namespace AgileTracker.TasksService.Infrastructure.Persistance
{
    using System.Reflection;

    using AgileTracker.TasksService.Domain.Models;
    using AgileTracker.TasksService.Domain.Models.Entities;
    using AgileTracker.TasksService.Domain.Models.ValueObjects;

    using Microsoft.EntityFrameworkCore;

    public class AgileTrackerTasksDbContext: DbContext
    {
        public AgileTrackerTasksDbContext(DbContextOptions<AgileTrackerTasksDbContext> options)
            :base(options)
        {
        }

        public DbSet<ProjectGroup> ProjectGroups { get; set; } = default!;

        public DbSet<ProjectGroupMember> ProjectGroupMembers { get; set; } = default!;

        public DbSet<ProjectGroupInvitation> ProjectGroupInvitations { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
