namespace AgileTracker.Client.Startup.Models.Tasks.Projects.Sprint
{
    using System;

    using AgileTracker.Client.Application.Features.Tasks.Queries.GetSprint;
    using AgileTracker.Common.Application.Mapping;

    public class GetSprintTaskViewModel:IMapFrom<SprintTaskOutputModel>
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string AssignedToMemberId { get; set; } = default!;

        public int PointsEstimate { get; set; }

        public GetSprintTaskStatusViewModel Status { get; set; } = default!;

        public DateTime StartedOn { get; set; }

        public DateTime FinishedOn { get; set; }

        public bool IsFinished { get; set; }
    }
}