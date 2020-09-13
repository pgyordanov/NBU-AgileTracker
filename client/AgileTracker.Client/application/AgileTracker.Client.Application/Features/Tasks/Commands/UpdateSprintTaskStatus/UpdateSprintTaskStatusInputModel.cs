namespace AgileTracker.Client.Application.Features.Tasks.Commands.UpdateSprintTaskStatus
{
    public class UpdateSprintTaskStatusInputModel
    {
        public UpdateSprintTaskStatusInputModel(int projectGroupId, int projectId, int sprintId, int taskId, string taskStatus)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.SprintId = sprintId;
            this.TaskId = taskId;
            this.TaskStatus = taskStatus;
        }

        public int ProjectGroupId { get; }

        public int ProjectId { get; }

        public int SprintId { get; }

        public int TaskId { get; }

        public string TaskStatus { get; }
    }
}
