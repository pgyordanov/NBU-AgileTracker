namespace AgileTracker.Client.Startup.Models.Tasks.ProjectGroups.Group
{
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroup;
    using AgileTracker.Common.Application.Mapping;

    public class ProjectGroupMemberViewModel: IMapFrom<ProjectGroupMemberOutputModel>
    {
        public int Id { get; set; }

        public string MemberId { get; set; }

        public string UserName { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public bool IsOwner { get; set; }
    }
}
