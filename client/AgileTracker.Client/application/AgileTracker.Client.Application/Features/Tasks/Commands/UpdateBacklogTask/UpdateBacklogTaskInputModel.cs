namespace AgileTracker.Client.Application.Features.Tasks.Commands.UpdateBacklogTask
{
    public class UpdateBacklogTaskInputModel
    {
        public UpdateBacklogTaskInputModel(
               int projectGroupId,
               int projectId,
               int taskId,
               string title,
               string description,
               int pointsEstimate,
               string assignedToMemberId)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.TaskId = taskId;
            this.Title = title;
            this.Description = description;
            this.PointsEstimate = pointsEstimate;
            this.AssignedToMemberId = assignedToMemberId;
        }

        public int ProjectGroupId { get; }

        public int ProjectId { get; }
        public int TaskId { get; }
        public string Title { get; }

        public string Description { get; }

        public int PointsEstimate { get; }

        public string AssignedToMemberId { get; }
    }
}
