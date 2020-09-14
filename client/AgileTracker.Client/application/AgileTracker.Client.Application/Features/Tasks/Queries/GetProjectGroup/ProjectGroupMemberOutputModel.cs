namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroup
{
    public class ProjectGroupMemberOutputModel
    {
        public int Id { get; set; }

        public string MemberId { get; set; } = default!;

        public string UserName { get; set; } = default!;

        public string Firstname { get; set; } = default!;

        public string Lastname { get; set; } = default!;

        public bool IsOwner { get; set; }
    }
}
