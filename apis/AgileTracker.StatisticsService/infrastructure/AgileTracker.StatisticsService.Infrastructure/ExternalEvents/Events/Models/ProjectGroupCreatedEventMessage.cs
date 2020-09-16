namespace AgileTracker.StatisticsService.Infrastructure.ExternalEvents.Events.Models
{
    public class ProjectGroupCreatedEventMessage
    {
        public int ProjectGroupId { get; set; }

        public string OwnerId { get; set; } = default!;

        public string ProjectGroupName { get; set; } = default!;
    }
}
