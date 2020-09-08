namespace AgileTracker.TasksService.Application.Features.Commands.CreateProjectGroup
{
    public class CreateProjectGroupOutputModel
    {
        public CreateProjectGroupOutputModel(int groupId)
        {
            this.GroupId = groupId;
        }

        public int GroupId { get; }
    }
}
