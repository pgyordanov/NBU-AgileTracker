namespace AgileTracker.TasksService.Application.Features.Commands.CreateProject
{
    public class CreateProjectInputModel
    {
        public CreateProjectInputModel(int projectGroupId, string ownerId, string title)
        {
            this.ProjectGroupId = projectGroupId;
            this.OwnerId = ownerId;
            this.Title = title;
        }

        public int ProjectGroupId { get; }

        public string OwnerId { get; }

        public string Title { get; }
    }
}
