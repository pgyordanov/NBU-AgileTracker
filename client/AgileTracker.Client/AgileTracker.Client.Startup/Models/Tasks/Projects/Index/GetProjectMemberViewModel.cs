namespace AgileTracker.Client.Startup.Models.Tasks.Projects.Index
{
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProject;
    using AgileTracker.Common.Application.Mapping;

    public class GetProjectMemberViewModel: IMapFrom<ProjectMemberOutputModel>
    {
        public int Id { get; private set; }

        public string MemberId { get; private set; }

        public string UserName { get; private set; }

        public string Firstname { get; private set; }

        public string Lastname { get; private set; }

        public bool IsOwner { get; private set; }
    }
}
