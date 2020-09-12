namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetProject
{
    using System;
    using System.Collections.Generic;

    public class ProjectSprintOutputModel
    {
        public int Id { get; set; }

        public IEnumerable<ProjectTaskOutputModel> SprintBacklog { get; set; }

        public IEnumerable<ProjectTaskStatusOutputModel> TaskStatuses { get; set; }

        public int DurationWeeks { get; set; }

        public DateTime StartedOn { get; set; }

        public DateTime FinishedOn { get; set; }

        public bool IsFinished { get; private set; }
    }
}