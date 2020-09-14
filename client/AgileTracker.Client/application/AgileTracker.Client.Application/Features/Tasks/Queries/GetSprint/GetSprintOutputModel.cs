namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetSprint
{
    using System;
    using System.Collections.Generic;

    public class GetSprintOutputModel
    {
        public int Id { get; set; }

        public int DurationWeeks { get; set; }

        public IEnumerable<SprintTaskOutputModel> SprintBacklog { get; set; } = default!;

        public IEnumerable<SprintTaskStatusOutputModel> TaskStatuses { get; set; } = default!;

        public DateTime StartedOn { get; set; }

        public DateTime FinishedOn { get; set; }

        public bool IsFinished { get; set; }
    }
}
