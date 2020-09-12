namespace AgileTracker.Client.Startup.Models.Tasks.Projects.Index
{
    using System;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProject;
    using AgileTracker.Common.Application.Mapping;

    public class GetProjectTaskViewModel: IMapFrom<ProjectTaskOutputModel>
    {
        public int Id { get; private set; }

        public string Title { get; private set; } = default!;

        public string Description { get; private set; } = default!;

        public string AssignedToMemberId { get; private set; } = default!;

        public int PointsEstimate { get; private set; }

        public GetProjectTaskStatusViewModel Status { get; private set; } = default!;

        public DateTime StartedOn { get; private set; }

        public DateTime FinishedOn { get; private set; }

        public bool IsFinished { get; private set; }
    }
}