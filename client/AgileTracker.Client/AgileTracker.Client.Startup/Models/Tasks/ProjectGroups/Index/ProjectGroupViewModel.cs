namespace AgileTracker.Client.Startup.Models.Tasks.ProjectGroups.Index
{
    using System.Collections.Generic;

    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroups;
    using AgileTracker.Common.Application.Mapping;

    public class ProjectGroupViewModel: IMapFrom<GetProjectGroupsOutputModel>
    {
        public int Id { get; set; }

        public string GroupName { get; set; } = default!;

        public List<ProjectGroupMemberViewModel> Members { get; set; } = default!;
    }
}
