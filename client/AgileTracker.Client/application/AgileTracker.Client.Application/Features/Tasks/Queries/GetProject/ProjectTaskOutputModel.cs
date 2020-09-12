namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetProject
{
    using System;

    public class ProjectTaskOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string AssignedToMemberId { get; set; } = default!;

        public int PointsEstimate { get; set; }

        public ProjectTaskStatusOutputModel Status { get; set; } = default!;

        public DateTime StartedOn { get; set; }

        public DateTime FinishedOn { get; set; }

        public bool IsFinished { get; set; }
    }
}