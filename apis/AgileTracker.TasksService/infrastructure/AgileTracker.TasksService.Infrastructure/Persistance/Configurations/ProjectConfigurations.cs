namespace AgileTracker.TasksService.Infrastructure.Persistance.Configurations
{

    using AgileTracker.TasksService.Domain.Models.Entities;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProjectConfigurations : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .HasMany(t => t.Backlog)
                .WithOne()
                .IsRequired();

            builder
                .HasMany(t => t.Sprints)
                .WithOne()
                .IsRequired();
        }
    }
}
