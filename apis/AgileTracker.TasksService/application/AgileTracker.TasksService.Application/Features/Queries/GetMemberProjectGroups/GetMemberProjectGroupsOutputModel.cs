namespace AgileTracker.TasksService.Application.Features.Queries.GetMemberProjectGroups
{
    using System.Collections.Generic;

    using AgileTracker.Common.Application.Mapping;
    using AgileTracker.TasksService.Domain.Models;

    public class GetMemberProjectGroupsOutputModel: IMapFrom<ProjectGroup>
    {
        public int Id { get; private set; }

        public string GroupName { get; private set; }

        public List<ProjectGroupMemberOuputModel> Members { get; private set; }

        public List<ProjectOutputModel> Projects { get; private set; }
    }
}
