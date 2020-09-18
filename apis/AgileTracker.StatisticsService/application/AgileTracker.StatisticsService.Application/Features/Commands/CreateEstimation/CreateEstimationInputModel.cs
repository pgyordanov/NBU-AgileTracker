namespace AgileTracker.StatisticsService.Application.Features.Commands.CreateEstimation
{
    using System;

    public class CreateEstimationInputModel
    {
        public CreateEstimationInputModel(int projectGroupId, int projectId, int taskId, string memberId, DateTime startedOn, DateTime estimatedToFinishOn)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.TaskId = taskId;
            this.EstimatedByMemberId = memberId;
            this.StartedOn = startedOn;
            this.EstimatedToFinishOn = estimatedToFinishOn;
        }

        public int ProjectGroupId { get; }

        public int ProjectId { get; }

        public int TaskId { get; }

        public string EstimatedByMemberId { get; }

        public DateTime StartedOn { get; }

        public DateTime EstimatedToFinishOn { get; }
    }
}
