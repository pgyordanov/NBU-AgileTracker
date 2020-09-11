namespace AgileTracker.TasksService.Application.Features.Queries.GetMemberProjectGroups
{
    using AgileTracker.Common.Application.Mapping;
    using AgileTracker.TasksService.Domain.Models.Entities;

    public class ProjectOutputModel: IMapFrom<Project>
    {
        public int Id { get; private set; }

        public string Title { get; private set; }
    }
}
