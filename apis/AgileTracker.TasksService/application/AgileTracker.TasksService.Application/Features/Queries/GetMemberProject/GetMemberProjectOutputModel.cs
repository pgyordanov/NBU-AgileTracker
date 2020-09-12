namespace AgileTracker.TasksService.Application.Features.Queries.GetMemberProject
{
    using System.Collections.Generic;

    using AgileTracker.Common.Application.Mapping;
    using AgileTracker.TasksService.Domain.Models.Entities;

    public class GetMemberProjectOutputModel: IMapFrom<Project>
    {
        public int Id { get; private set; }

        public string Title { get; private set; }

        public IEnumerable<ProjectTaskOutputModel> Backlog { get; private set; }

        public IEnumerable<ProjectSprintOutputModel> Sprints { get; private set; }
    }
}
