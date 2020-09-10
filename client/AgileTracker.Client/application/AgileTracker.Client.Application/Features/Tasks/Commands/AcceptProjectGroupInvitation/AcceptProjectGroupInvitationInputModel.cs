namespace AgileTracker.Client.Application.Features.Tasks.Commands.AcceptProjectGroupInvitation
{
    public class AcceptProjectGroupInvitationInputModel
    {
        public AcceptProjectGroupInvitationInputModel(int groupId)
        {
            this.GroupId = groupId;
        }

        public int GroupId { get; }
    }
}
