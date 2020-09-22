namespace AgileTracker.Client.Application.Features.Statistics.Commands.CreateTaskEstimation
{
    using System;

    public class CreateTaskEstimationInputModel
    {
        public CreateTaskEstimationInputModel(int projectGroupId, int projectId, int taskId, DateTime startedOn, DateTime estimatedToFinishOn)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.TaskId = taskId;
            this.StartedOn = startedOn;
            this.EstimatedToFinishOn = estimatedToFinishOn;
        }

        public int ProjectGroupId { get; }

        public int ProjectId { get; }

        public int TaskId { get; }

        public DateTime StartedOn { get; }

        public DateTime EstimatedToFinishOn { get; }
    }
}
