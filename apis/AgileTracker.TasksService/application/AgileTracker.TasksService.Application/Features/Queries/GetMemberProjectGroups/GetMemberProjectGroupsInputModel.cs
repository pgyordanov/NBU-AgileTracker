namespace AgileTracker.TasksService.Application.Features.Queries.GetMemberProjectGroups
{
    public class GetMemberProjectGroupsInputModel
    {
        public GetMemberProjectGroupsInputModel(string memberId)
        {
            this.MemberId = memberId;
        }

        public string MemberId { get; }
    }
}
