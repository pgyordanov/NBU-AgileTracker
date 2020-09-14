namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetProject
{
    using System.Collections.Generic;

    public class GetProjectOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = default!;

        public IEnumerable<ProjectTaskOutputModel> Backlog { get; set; } = default!;

        public IEnumerable<ProjectSprintOutputModel> Sprints { get; set; } = default!;

        public IEnumerable<ProjectMemberOutputModel> Members { get; set; } = default!;
    }
}
