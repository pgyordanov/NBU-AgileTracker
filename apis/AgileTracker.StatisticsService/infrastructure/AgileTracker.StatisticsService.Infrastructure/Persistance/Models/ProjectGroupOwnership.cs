namespace AgileTracker.StatisticsService.Infrastructure.Persistance.Models
{
    public class ProjectGroupOwnership
    {
        public int Id { get; private set; }

        public int ExternalProjectGroupId { get; private set; }

        public string OwnerId { get; private set; } = default!;
    }
}
