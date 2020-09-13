namespace AgileTracker.TasksService.Application.Features.Commands.UpdateSprintTaskStatus
{
    public class UpdateSprintTaskStatusInputModel
    {
        public UpdateSprintTaskStatusInputModel(
            int projectGroupId,
            int projectId,
            int taskId,
            int sprintId,
            string memberId,
            string taskStatus)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.TaskId = taskId;
            this.SprintId = sprintId;
            this.MemberId = memberId;
            this.TaskStatus = taskStatus;
        }

        public int ProjectGroupId { get; }

        public int ProjectId { get; }

        public int TaskId { get; }

        public int SprintId { get; }

        public string MemberId { get; }

        public string TaskStatus { get; }
    }
}
