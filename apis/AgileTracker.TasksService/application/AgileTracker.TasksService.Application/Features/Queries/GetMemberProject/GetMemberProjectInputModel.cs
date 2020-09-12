namespace AgileTracker.TasksService.Application.Features.Queries.GetMemberProject
{
    public class GetMemberProjectInputModel
    {
        public GetMemberProjectInputModel(int projectGroupId, int projectId, string memberId)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.MemberId = memberId;
        }

        public int ProjectGroupId { get; }

        public int ProjectId { get; }

        public string MemberId { get; }
    }
}
