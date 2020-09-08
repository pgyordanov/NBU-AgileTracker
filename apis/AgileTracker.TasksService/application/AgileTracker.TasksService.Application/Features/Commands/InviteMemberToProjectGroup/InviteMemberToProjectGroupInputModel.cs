namespace AgileTracker.TasksService.Application.Features.Commands.InviteMemberToProjectGroup
{
    public class InviteMemberToProjectGroupInputModel
    {
        public InviteMemberToProjectGroupInputModel(int projectGroupId, string invitedByMemberId, string memberId)
        {
            this.ProjectGroupId = projectGroupId;
            this.InvitedByMemberId = invitedByMemberId;
            this.MemberId = memberId;
        }

        public int ProjectGroupId { get; }

        public string InvitedByMemberId { get; }

        public string MemberId { get; }
    }
}
