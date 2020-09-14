namespace AgileTracker.Client.Application.Features.Tasks.Commands.RemoveProject
{
    public class RemoveProjectInputModel
    {
        public RemoveProjectInputModel(int projectGroupId, int projectId)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
        }

        public int ProjectGroupId { get; }

        public int ProjectId { get; }
    }
}
