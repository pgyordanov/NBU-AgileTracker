namespace AgileTracker.TasksService.Application.Features.Commands.RemoveSprint
{
    public class RemoveSprintInputModel
    {
        public RemoveSprintInputModel(
               int projectGroupId,
               int projectId,
               int sprintId,
               string memberId)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.SprintId = sprintId;
            this.MemberId = memberId;
        }

        public int ProjectGroupId { get; }

        public int ProjectId { get; }

        public int SprintId { get; }

        public string MemberId { get; }
    }
}
