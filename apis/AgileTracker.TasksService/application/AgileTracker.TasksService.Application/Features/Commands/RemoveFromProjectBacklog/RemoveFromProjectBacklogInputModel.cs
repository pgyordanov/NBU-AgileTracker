namespace AgileTracker.TasksService.Application.Features.Commands.RemoveFromProjectBacklog
{
    public class RemoveFromProjectBacklogInputModel
    {
        public RemoveFromProjectBacklogInputModel(int projectGroupId, int projectId, string memberId, int taskId)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.MemberId = memberId;
            this.TaskId = taskId;
        }

        public int ProjectGroupId { get; }

        public int ProjectId { get; }

        public string MemberId { get; }

        public int TaskId { get; }
    }
}
