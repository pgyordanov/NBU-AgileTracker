﻿namespace AgileTracker.StatisticsService.Application.Features.Queries.GetTaskEstimations
{
    public class GetTaskEstimationsInputModel
    {
        public GetTaskEstimationsInputModel(int? projectGroupId, int? projectId, int? taskId, bool? onlyCompleted, string memberId)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.TaskId = taskId;
            this.OnlyCompleted = onlyCompleted;
            this.MemberId = memberId;
        }

        public int? ProjectGroupId { get; }

        public int? ProjectId { get; }

        public int? TaskId { get; }

        public bool? OnlyCompleted { get; }

        public string MemberId { get; }
    }
}
