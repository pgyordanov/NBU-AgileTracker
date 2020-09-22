namespace AgileTracker.Client.Application.Features.Statistics.Commands.UpdateTaskEstimation
{
    using System;

    public class UpdateTaskEstimationInputModel
    {
        public UpdateTaskEstimationInputModel(int projectGroupId, int projectId, int taskId, DateTime estimatedToFinishOn)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.TaskId = taskId;
            this.EstimatedToFinishOn = estimatedToFinishOn;
        }

        public int ProjectGroupId { get; }

        public int ProjectId { get; }

        public int TaskId { get; }

        public DateTime EstimatedToFinishOn { get; }
    }
}
