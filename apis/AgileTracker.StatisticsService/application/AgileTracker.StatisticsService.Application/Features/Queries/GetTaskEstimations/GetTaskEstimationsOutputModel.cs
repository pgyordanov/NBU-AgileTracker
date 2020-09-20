namespace AgileTracker.StatisticsService.Application.Features.Queries.GetTaskEstimations
{
    using System;

    using AgileTracker.Common.Application.Mapping;
    using AgileTracker.StatisticsService.Domain.Models.TaskEstimation;

    public class GetTaskEstimationsOutputModel: IMapFrom<TaskEstimation>
    {
        public int TaskId { get; private set; }

        public int ProjectGroupId { get; private set; }

        public int ProjectId { get; private set; }

        public string EstimatedByMemberId { get; private set; } = default!;

        public DateTime StartedOn { get; private set; }

        public DateTime EstimatedToFinishOn { get; private set; }

        public DateTime ActuallyFinishedOn { get; private set; }
    }
}
