namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetProject
{
    public class GetProjectInputModel
    {
        public GetProjectInputModel(int projectGroupId, int projectId)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
        }

        public int ProjectGroupId { get; }

        public int ProjectId { get; }
    }
}
