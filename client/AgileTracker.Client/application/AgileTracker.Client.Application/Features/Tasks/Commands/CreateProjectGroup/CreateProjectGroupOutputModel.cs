namespace AgileTracker.Client.Application.Features.Tasks.Commands.CreateProjectGroup
{
    public class CreateProjectGroupOutputModel
    {
        public CreateProjectGroupOutputModel(string groupId)
        {
            this.GroupId = groupId;
        }

        public string GroupId { get; }
    }
}
