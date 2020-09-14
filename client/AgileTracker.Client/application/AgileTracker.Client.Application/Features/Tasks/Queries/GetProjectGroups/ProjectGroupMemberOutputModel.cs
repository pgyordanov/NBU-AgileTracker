namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroups
{
    public class ProjectGroupMemberOutputModel
    {
        public int Id { get; set; }

        public string MemberId { get; set; } = default!;

        public bool IsOwner { get; set; }
    }
}
