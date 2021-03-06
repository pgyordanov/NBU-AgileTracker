﻿namespace AgileTracker.TasksService.Application.Features.Queries.GetMemberProject
{
    using System;
    using System.Collections.Generic;

    using AgileTracker.Common.Application.Mapping;
    using AgileTracker.TasksService.Domain.Models.Entities;

    public class ProjectSprintOutputModel: IMapFrom<Sprint>
    {
        public int Id { get; private set; }

        public IEnumerable<ProjectTaskOutputModel> SprintBacklog { get; private set; }

        public IEnumerable<ProjectTaskStatusOutputModel> TaskStatuses { get; private set; }

        public int DurationWeeks { get; private set; }

        public DateTime StartedOn { get; private set; }

        public DateTime FinishedOn { get; private set; }

        public bool IsFinished { get; private set; }
    }
}