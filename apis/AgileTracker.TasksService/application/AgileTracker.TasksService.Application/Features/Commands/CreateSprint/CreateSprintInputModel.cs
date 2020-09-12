namespace AgileTracker.TasksService.Application.Features.Commands.CreateSprint
{
    using System;
    using System.Collections.Generic;

    public class CreateSprintInputModel
    {
        public CreateSprintInputModel(int projectGroupId, int projectId, string memberId, IEnumerable<int> taskIds, DateTime startsOn, int durationWeeks)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.MemberId = memberId;
            this.TaskIds = taskIds;
            this.StartsOn = startsOn;
            this.DurationWeeks = durationWeeks;
        }

        public int ProjectGroupId { get; }

        public int ProjectId { get; }

        public string MemberId { get; }

        public IEnumerable<int> TaskIds { get; }

        public DateTime StartsOn { get; }

        public int DurationWeeks { get; }
    }
}
