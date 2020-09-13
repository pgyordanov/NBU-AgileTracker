namespace AgileTracker.TasksService.Application.Features.Queries.GetMemberSprint
{
    using System;
    using System.Collections.Generic;

    using AgileTracker.Common.Application.Mapping;
    using AgileTracker.TasksService.Domain.Models.Entities;

    public class GetMemberSprintOutputModel : IMapFrom<Sprint>
    {
        public int Id { get; set; }

        public int DurationWeeks { get; set; }

        public IEnumerable<SprintTaskOutputModel> SprintBacklog { get; set; }

        public IEnumerable<SprintTaskStatusOutputModel> TaskStatuses { get; set; }

        public DateTime StartedOn { get; set; }

        public DateTime FinishedOn { get; set; }

        public bool IsFinished { get; set; }
    }
}
