namespace AgileTracker.Client.Startup.Models.Tasks.Projects.Index
{
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProject;
    using AgileTracker.Common.Application.Mapping;

    public class GetProjectMemberViewModel: IMapFrom<ProjectMemberOutputModel>
    {
        public int Id { get; private set; }

        public string MemberId { get; private set; } = default!;

        public string UserName { get; private set; } = default!;

        public string Firstname { get; private set; } = default!;

        public string Lastname { get; private set; } = default!;

        public bool IsOwner { get; private set; }
    }
}
