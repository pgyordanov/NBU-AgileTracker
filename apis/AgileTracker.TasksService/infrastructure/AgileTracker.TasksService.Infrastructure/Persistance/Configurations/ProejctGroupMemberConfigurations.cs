namespace AgileTracker.TasksService.Infrastructure.Persistance.Configurations
{
    using AgileTracker.TasksService.Domain.Models.Entities;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProejctGroupMemberConfigurations : IEntityTypeConfiguration<ProjectGroupMember>
    {
        public void Configure(EntityTypeBuilder<ProjectGroupMember> builder)
        {
            builder.HasKey(m => m.Id);
        }
    }
}
