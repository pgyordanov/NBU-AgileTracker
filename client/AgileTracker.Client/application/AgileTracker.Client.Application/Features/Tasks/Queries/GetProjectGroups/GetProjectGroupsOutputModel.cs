namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroups
{
    using System.Collections.Generic;

    public class GetProjectGroupsOutputModel
    {
        public int Id { get; set; }

        public string GroupName { get; set; }

        public List<ProjectGroupMemberOutputModel> Members { get; set; }
    }
}
