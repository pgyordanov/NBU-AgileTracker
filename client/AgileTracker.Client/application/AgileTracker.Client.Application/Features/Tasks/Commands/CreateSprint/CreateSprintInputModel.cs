namespace AgileTracker.Client.Application.Features.Tasks.Commands.CreateSprint
{
    using System;
    using System.Collections.Generic;

    public class CreateSprintInputModel
    {
        public CreateSprintInputModel(int projectGroupId, int projectId, int durationWeeks, IEnumerable<int> taskIds, DateTime startsOn)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.DurationWeeks = durationWeeks;
            this.TaskIds = taskIds;
            this.StartsOn = startsOn;
        }

        public int ProjectGroupId { get; }

        public int ProjectId { get; }

        public int DurationWeeks { get; }

        public IEnumerable<int> TaskIds { get; }

        public DateTime StartsOn { get; }
    }
}
