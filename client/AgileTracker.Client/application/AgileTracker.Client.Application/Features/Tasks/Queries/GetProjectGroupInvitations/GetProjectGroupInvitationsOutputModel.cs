namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroupInvitations
{
    public class GetProjectGroupInvitationsOutputModel
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; } = default!;
    }
}
