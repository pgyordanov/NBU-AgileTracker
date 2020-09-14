namespace AgileTracker.Client.Startup.Models.Tasks.Projects.Index
{
    using System;
    using System.Collections.Generic;

    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProject;
    using AgileTracker.Common.Application.Mapping;

    public class GetProjectSprintViewModel: IMapFrom<ProjectSprintOutputModel>
    {
        public int Id { get; private set; }

        public IEnumerable<GetProjectTaskViewModel> SprintBacklog { get; private set; } = default!;

        public IEnumerable<GetProjectTaskStatusViewModel> TaskStatuses { get; private set; } = default!;

        public int DurationWeeks { get; private set; }

        public DateTime StartedOn { get; private set; }

        public DateTime FinishedOn { get; private set; }

        public bool IsFinished { get; private set; }
    }
}