namespace AgileTracker.Client.Application.Features.Tasks.Commands.InviteProjectGroupMember
{
    public class InviteProjectGroupMemberInputModel
    {
        public InviteProjectGroupMemberInputModel(int projectGroupId, string memberEmail)
        {
            this.ProjectGroupId = projectGroupId;
            this.MemberEmail = memberEmail;
        }

        public int ProjectGroupId { get; }

        public string MemberEmail { get; }
    }
}
