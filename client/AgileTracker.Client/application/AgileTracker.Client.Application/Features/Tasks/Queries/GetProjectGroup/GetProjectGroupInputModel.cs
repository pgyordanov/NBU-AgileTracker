namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroup
{
    public class GetProjectGroupInputModel
    {
        public GetProjectGroupInputModel(int groupId)
        {
            this.GroupId = groupId;
        }

        public int GroupId { get; }
    }
}
