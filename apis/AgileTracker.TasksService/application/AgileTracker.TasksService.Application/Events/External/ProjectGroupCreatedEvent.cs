namespace AgileTracker.TasksService.Application.Events.External
{
    public class ProjectGroupCreatedEvent
    {
        public ProjectGroupCreatedEvent(int projectGroupId, string ownerId, string projectGroupName)
        {
            this.ProjectGroupId = projectGroupId;
            this.OwnerId = ownerId;
            this.ProjectGroupName = projectGroupName;
        }

        public int ProjectGroupId { get; }

        public string OwnerId { get; }

        public string ProjectGroupName { get; }
    }
}
