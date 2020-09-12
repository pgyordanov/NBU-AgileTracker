namespace AgileTracker.TasksService.Application.Features.Commands.CreateSprint
{
    public class CreateSprintOutputModel
    {
        public CreateSprintOutputModel(int sprintId)
        {
            this.SprintId = sprintId;
        }

        public int SprintId { get; }
    }
}
