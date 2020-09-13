namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetSprint
{
    public class GetSprintInputModel
    {
        public GetSprintInputModel(int projectGroupId, int projectId, int sprintId)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.SprintId = sprintId;
        }

        public int ProjectGroupId { get; }

        public int ProjectId { get; }

        public int SprintId { get; }
    }
}
