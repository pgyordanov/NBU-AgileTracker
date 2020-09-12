namespace AgileTracker.Client.Startup.Models.Tasks.Projects.Index
{
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProject;
    using AgileTracker.Common.Application.Mapping;

    public class GetProjectTaskStatusViewModel: IMapFrom<ProjectTaskStatusOutputModel>
    {
        public string Title { get; private set; }

        public string IsEnd { get; private set; }
    }
}