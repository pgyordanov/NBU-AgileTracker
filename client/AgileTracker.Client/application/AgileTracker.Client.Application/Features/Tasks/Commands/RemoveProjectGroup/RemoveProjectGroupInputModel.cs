namespace AgileTracker.Client.Application.Features.Tasks.Commands.RemoveProjectGroup
{
    public class RemoveProjectGroupInputModel
    {
        public RemoveProjectGroupInputModel(int projectGroupId)
        {
            this.ProjectGroupId = projectGroupId;
        }

        public int ProjectGroupId { get; }
    }
}
