namespace AgileTracker.Client.Startup.Models.Tasks.Projects.Index
{
    using System.Collections.Generic;

    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProject;
    using AgileTracker.Common.Application.Mapping;

    public class GetProjectViewModel: IMapFrom<GetProjectOutputModel>
    {
        public int Id { get; private set; }

        public int ProjectGroupId { get; set; }

        public string Title { get; private set; }

        public IEnumerable<GetProjectTaskViewModel> Backlog { get; private set; }

        public IEnumerable<GetProjectSprintViewModel> Sprints { get; private set; }
    }
}
