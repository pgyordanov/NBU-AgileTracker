namespace AgileTracker.TasksService.Infrastructure.Persistance.Configurations
{
    using AgileTracker.TasksService.Domain.Models.Entities;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TaskConfigurations: IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(g => g.Id);

            builder
                .OwnsOne(t => t.Description, description =>
                  {
                      description.WithOwner();
                  });

            builder
                .OwnsOne(t => t.Status, status =>
                {
                    status.WithOwner();
                });
        }
    }
}
