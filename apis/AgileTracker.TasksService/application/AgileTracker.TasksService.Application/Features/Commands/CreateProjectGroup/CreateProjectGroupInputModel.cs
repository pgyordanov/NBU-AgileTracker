namespace AgileTracker.TasksService.Application.Features.Commands.CreateProjectGroup
{
    public class CreateProjectGroupInputModel
    {
        public CreateProjectGroupInputModel(string groupName, string ownerId)
        {
            this.GroupName = groupName;
            this.OwnerId = ownerId;
        }

        public string GroupName { get; }

        public string OwnerId { get; }
    }
}
