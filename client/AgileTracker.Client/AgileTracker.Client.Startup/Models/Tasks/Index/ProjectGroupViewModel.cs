namespace AgileTracker.Client.Startup.Models.Tasks.Index
{
    using System.Collections.Generic;

    public class ProjectGroupViewModel
    {
        public int Id { get; set; }

        public string GroupName { get; set; }

        public List<ProjectGroupMemberViewModel> Members { get; set; }
    }
}
