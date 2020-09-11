namespace AgileTracker.Client.Startup.Models.Tasks.ProjectGroups.Group
{
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroup;
    using AgileTracker.Common.Application.Mapping;

    public class ProjectGroupProjectViewModel: IMapFrom<ProjectGroupProjectOutputModel>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
