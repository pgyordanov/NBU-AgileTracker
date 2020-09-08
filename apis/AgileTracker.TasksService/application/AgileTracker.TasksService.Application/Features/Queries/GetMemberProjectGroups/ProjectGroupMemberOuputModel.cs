namespace AgileTracker.TasksService.Application.Features.Queries.GetMemberProjectGroups
{
    using AgileTracker.Common.Application.Mapping;
    using AgileTracker.TasksService.Domain.Models.Entities;

    public class ProjectGroupMemberOuputModel: IMapFrom<ProjectGroupMember>
    {
        public int Id { get; private set; }

        public string MemberId { get; private set; }

        public bool IsOwner { get; private set; }
    }
}
