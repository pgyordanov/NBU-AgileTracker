namespace AgileTracker.Client.Application.Features.Statistics.Queries.GetTaskEstimations
{
    using System;

    public class GetTaskEstimationsOutputModel
    {
        public int TaskId { get; set; }

        public int ProjectGroupId { get; set; }

        public int ProjectId { get; set; }

        public string EstimatedByMemberId { get; set; } = default!;

        public DateTime StartedOn { get; set; }

        public DateTime EstimatedToFinishOn { get; set; }

        public DateTime ActuallyFinishedOn { get; set; }
    }
}