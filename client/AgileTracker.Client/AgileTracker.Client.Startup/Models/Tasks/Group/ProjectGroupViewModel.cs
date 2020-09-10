namespace AgileTracker.Client.Startup.Models.Tasks.Group
{
    using System.Collections.Generic;

    using AgileTracker.Client.Startup.Models.Tasks.InviteProjectGroupMember;

    public class ProjectGroupViewModel
    {
        public int Id { get; set; }

        public string GroupName { get; set; }

        public IEnumerable<ProjectGroupMemberViewModel> Members { get; set; }

        public InviteProjectGroupMemberViewModel InvitationViewModel { get; set; }
    }
}
