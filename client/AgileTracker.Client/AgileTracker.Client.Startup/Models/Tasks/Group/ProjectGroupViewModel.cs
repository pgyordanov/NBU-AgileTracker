namespace AgileTracker.Client.Startup.Models.Tasks.Group
{
    using System.Collections.Generic;

    public class ProjectGroupViewModel
    {
        public int Id { get; set; }

        public string GroupName { get; set; }

        public IEnumerable<ProjectGroupMemberViewModel> Members { get; set; }
    }
}
