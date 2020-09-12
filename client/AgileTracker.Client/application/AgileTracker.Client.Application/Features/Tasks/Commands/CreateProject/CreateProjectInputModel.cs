namespace AgileTracker.Client.Application.Features.Tasks.Commands.CreateProject
{
    public class CreateProjectInputModel
    {
        public CreateProjectInputModel(int projectGroupId, string projectTitle)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectTitle = projectTitle;
        }

        public int ProjectGroupId { get; }

        public string ProjectTitle { get; }
    }
}
