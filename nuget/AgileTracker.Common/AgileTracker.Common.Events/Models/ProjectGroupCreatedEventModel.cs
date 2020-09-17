namespace AgileTracker.Common.Events.Models
{
    public class ProjectGroupCreatedEventModel
    {
        public int ProjectGroupId { get; set; }

        public string ProjectGroupOwnerId { get; set; } = default!;

        public string ProjectGroupName { get; set; } = default!;
    }
}
