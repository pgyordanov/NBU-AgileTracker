namespace AgileTracker.Client.Application.Features.Tasks.Commands
{
    public class CreateProjectGroupInputModel
    {
        public CreateProjectGroupInputModel(string groupName)
        {
            this.GroupName = groupName;
        }

        public string GroupName { get; }
    }
}
