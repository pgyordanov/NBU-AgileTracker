namespace AgileTracker.StatisticsService.Infrastructure.Persistance.Configurations
{
    using AgileTracker.StatisticsService.Infrastructure.Persistance.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProjectGroupOwnershipConfigurations : IEntityTypeConfiguration<ProjectGroupOwnership>
    {
        public void Configure(EntityTypeBuilder<ProjectGroupOwnership> builder)
        {
            builder.HasKey(g => g.Id);

            builder.HasIndex(g => g.ExternalProjectGroupId).IsUnique();
        }
    }
}
