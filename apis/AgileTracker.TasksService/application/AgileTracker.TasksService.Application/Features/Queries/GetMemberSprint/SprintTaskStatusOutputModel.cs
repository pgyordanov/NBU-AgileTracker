﻿namespace AgileTracker.TasksService.Application.Features.Queries.GetMemberSprint
{
    using AgileTracker.Common.Application.Mapping;
    using AgileTracker.TasksService.Domain.Models.ValueObjects;

    public class SprintTaskStatusOutputModel: IMapFrom<TaskStatus>
    {
        public string Title { get; private set; }

        public string IsEnd { get; private set; }
    }
}