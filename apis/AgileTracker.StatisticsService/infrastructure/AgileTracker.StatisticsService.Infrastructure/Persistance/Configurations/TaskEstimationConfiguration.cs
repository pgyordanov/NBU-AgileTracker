namespace AgileTracker.StatisticsService.Infrastructure.Persistance.Configurations
{
    using AgileTracker.StatisticsService.Domain.Models.TaskEstimation;
    using AgileTracker.StatisticsService.Infrastructure.Persistance.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TaskEstimationConfiguration : IEntityTypeConfiguration<TaskEstimation>
    {
        public void Configure(EntityTypeBuilder<TaskEstimation> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.ProjectGroupId).IsRequired();
            builder.Property(t => t.ProjectId).IsRequired();
            builder.Property(t => t.TaskId).IsRequired();

            builder
                .HasOne<ProjectGroupOwnership>()
                .WithMany()
                .HasPrincipalKey(group => group.ExternalProjectGroupId)
                .HasForeignKey(estimation => estimation.ProjectGroupId);
        }
    }
}
