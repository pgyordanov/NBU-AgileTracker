namespace AgileTracker.TasksService.Infrastructure.Persistance.Configurations
{
    using AgileTracker.TasksService.Domain.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProjectGroupConfigurations : IEntityTypeConfiguration<ProjectGroup>
    {
        public void Configure(EntityTypeBuilder<ProjectGroup> builder)
        {
            builder.HasKey(g => g.Id);

            builder
                .HasMany(g => g.Members)
                .WithOne()
                .IsRequired();

            builder.HasMany(g => g.Projects)
                .WithOne()
                .IsRequired();

            builder
                .OwnsMany(g => g.Invitations)
                .WithOwner();
        }
    }
}
