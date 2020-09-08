namespace AgileTracker.TasksService.Application.Features.Commands.AddMemberToProjectGroup
{
    public class AddMemberToProjectGroupInputModel
    {
        public AddMemberToProjectGroupInputModel(int projectGroupId, string memberId)
        {
            this.ProjectGroupId = projectGroupId;
            this.MemberId = memberId;
        }

        public int ProjectGroupId { get; }

        public string MemberId { get; }
    }
}
