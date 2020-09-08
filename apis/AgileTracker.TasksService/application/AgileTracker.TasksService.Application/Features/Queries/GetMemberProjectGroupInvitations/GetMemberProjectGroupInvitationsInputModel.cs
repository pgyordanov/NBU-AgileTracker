namespace AgileTracker.TasksService.Application.Features.Queries.GetMemberProjectGroupInvitations
{
    public class GetMemberProjectGroupInvitationsInputModel
    {
        public GetMemberProjectGroupInvitationsInputModel(string memberId)
        {
            this.MemberId = memberId;
        }

        public string MemberId { get; }
    }
}
