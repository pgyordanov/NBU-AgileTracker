namespace AgileTracker.Client.Startup.Models.Tasks.ProjectGroups.Group
{
    using System.Collections.Generic;

    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroup;
    using AgileTracker.Common.Application.Mapping;

    public class ProjectGroupViewModel: IMapFrom<GetProjectGroupOutputModel>
    {
        public int Id { get; set; }

        public string GroupName { get; set; } = default!;

        public IEnumerable<ProjectGroupMemberViewModel> Members { get; set; } = default!;

        public IEnumerable<ProjectGroupProjectViewModel> Projects { get; set; } = default!;
    }
}
