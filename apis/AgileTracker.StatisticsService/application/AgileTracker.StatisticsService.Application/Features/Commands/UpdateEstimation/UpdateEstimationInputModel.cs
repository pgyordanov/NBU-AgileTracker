namespace AgileTracker.StatisticsService.Application.Features.Commands.UpdateEstimation
{
    using System;

    public class UpdateEstimationInputModel
    {
        public UpdateEstimationInputModel(int projectGroupId, int projectId, int taskId, string memberId, DateTime estimatedToFinishOn)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.TaskId = taskId;
            this.MemberId = memberId;
            this.EstimatedToFinishOn = estimatedToFinishOn;
        }

        public int ProjectGroupId { get; }

        public int ProjectId { get; }

        public int TaskId { get; }

        public string MemberId { get; }

        public DateTime EstimatedToFinishOn { get; }
    }
}
