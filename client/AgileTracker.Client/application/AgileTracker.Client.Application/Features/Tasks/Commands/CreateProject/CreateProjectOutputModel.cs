namespace AgileTracker.Client.Application.Features.Tasks.Commands.CreateProject
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
