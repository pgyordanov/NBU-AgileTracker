namespace AgileTracker.Client.Startup.Models.Tasks.ProjectGroups.Index
{
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroups;
    using AgileTracker.Common.Application.Mapping;

    public class ProjectGroupMemberViewModel: IMapFrom<ProjectGroupMemberOutputModel>
    {
        public int Id { get; set; }

        public string MemberId { get; set; } = default!;

        public bool IsOwner { get; set; }
    }
}
