namespace AgileTracker.TasksService.Infrastructure.Persistance.Configurations
{
    using AgileTracker.TasksService.Domain.Models.Entities;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SprintConfiguration : IEntityTypeConfiguration<Sprint>
    {
        public void Configure(EntityTypeBuilder<Sprint> builder)
        {
            builder.HasKey(s => s.Id);

            builder
                .OwnsMany(s => s.TaskStatuses)
                .WithOwner();

            builder
                .HasMany(t => t.SprintBacklog)
                .WithOne();
        }
    }
}
