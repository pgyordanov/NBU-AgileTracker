namespace AgileTracker.TasksService.Application.Features.Commands.CreateProject
{
    public class CreateProjectOutputModel
    {
        public CreateProjectOutputModel(int projectId)
        {
            this.ProjectId = projectId;
        }

        public int ProjectId { get; }
    }
}
