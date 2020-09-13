﻿namespace AgileTracker.Client.Startup.Models.Tasks.Projects.Sprint
{
    using System;
    using System.Collections.Generic;

    using AgileTracker.Client.Application.Features.Tasks.Queries.GetSprint;
    using AgileTracker.Common.Application.Mapping;

    public class GetSprintViewModel: IMapFrom<GetSprintOutputModel>
    {
        public int Id { get; private set; }

        public int ProjectGroupId { get; set; }

        public int ProjectId { get; set; }

        public int DurationWeeks { get; set; }

        public IEnumerable<GetSprintTaskViewModel> SprintBacklog { get; set; }

        public IEnumerable<GetSprintTaskStatusViewModel> TaskStatuses { get; set; }

        public DateTime StartedOn { get; set; }

        public DateTime FinishedOn { get; set; }

        public bool IsFinished { get; set; }
    }
}
