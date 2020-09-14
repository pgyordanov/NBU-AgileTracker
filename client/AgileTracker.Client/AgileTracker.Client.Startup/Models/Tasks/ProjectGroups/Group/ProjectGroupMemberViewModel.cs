namespace AgileTracker.Client.Startup.Models.Tasks.ProjectGroups.Group
{
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroup;
    using AgileTracker.Common.Application.Mapping;

    public class ProjectGroupMemberViewModel: IMapFrom<ProjectGroupMemberOutputModel>
    {
        public int Id { get; set; }

        public string MemberId { get; set; } = default!;

        public string UserName { get; set; } = default!;

        public string Firstname { get; set; } = default!;

        public string Lastname { get; set; } = default!;

        public bool IsOwner { get; set; }
    }
}
