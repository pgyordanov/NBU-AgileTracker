namespace AgileTracker.TasksService.Application.Features.Queries.GetMemberProjectGroupInvitations
{
    public class GetMemberProjectGroupInvitationsOutputModel
    {
        public GetMemberProjectGroupInvitationsOutputModel(int groupId, string groupName)
        {
            this.GroupId = groupId;
            this.GroupName = groupName;
        }

        public int GroupId { get; }

        public string GroupName { get; }
    }
}
