namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetProject
{
    using System.Collections.Generic;

    public class GetProjectOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<ProjectTaskOutputModel> Backlog { get; set; }

        public IEnumerable<ProjectSprintOutputModel> Sprints { get; set; }

        public IEnumerable<ProjectMemberOutputModel> Members { get; set; }
    }
}
