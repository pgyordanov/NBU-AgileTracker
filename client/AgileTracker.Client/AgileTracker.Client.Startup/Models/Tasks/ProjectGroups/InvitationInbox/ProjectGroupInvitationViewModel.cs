namespace AgileTracker.Client.Startup.Models.Tasks.ProjectGroups.InvitationInbox
{
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroupInvitations;
    using AgileTracker.Common.Application.Mapping;

    public class ProjectGroupInvitationViewModel: IMapFrom<GetProjectGroupInvitationsOutputModel>
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; } = default!;
    }
}
