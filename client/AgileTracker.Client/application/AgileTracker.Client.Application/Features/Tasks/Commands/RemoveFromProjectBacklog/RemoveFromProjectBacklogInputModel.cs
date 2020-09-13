namespace AgileTracker.Client.Application.Features.Tasks.Commands.RemoveFromProjectBacklog
{
    public class RemoveFromProjectBacklogInputModel
    {
        public RemoveFromProjectBacklogInputModel(int projectGroupId, int projectId, int taskId)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.TaskId = taskId;
        }

        public int ProjectGroupId { get; }

        public int ProjectId { get; }

        public int TaskId { get; }
    }
}
