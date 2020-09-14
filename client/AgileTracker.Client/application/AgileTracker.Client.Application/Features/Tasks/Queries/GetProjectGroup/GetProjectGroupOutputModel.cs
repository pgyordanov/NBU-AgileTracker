namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroup
{
    using System.Collections.Generic;

    public class GetProjectGroupOutputModel
    {
        public int Id { get; set; }

        public string GroupName { get; set; } = default!;

        public IEnumerable<ProjectGroupMemberOutputModel> Members { get; set; } = default!;

        public IEnumerable<ProjectGroupProjectOutputModel> Projects { get; set; } = default!;
    }
}
