namespace AgileTracker.Client.Startup.Models.Tasks.Projects.Sprint
{
    using System;

    using AgileTracker.Client.Application.Features.Tasks.Queries.GetSprint;
    using AgileTracker.Common.Application.Mapping;

    public class GetSprintTaskViewModel:IMapFrom<SprintTaskOutputModel>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AssignedToMemberId { get; set; }

        public int PointsEstimate { get; set; }

        public GetSprintTaskStatusViewModel Status { get; set; }

        public DateTime StartedOn { get; set; }

        public DateTime FinishedOn { get; set; }

        public bool IsFinished { get; set; }
    }
}