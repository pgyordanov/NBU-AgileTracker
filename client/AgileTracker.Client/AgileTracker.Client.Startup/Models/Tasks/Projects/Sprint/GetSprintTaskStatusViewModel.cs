namespace AgileTracker.Client.Startup.Models.Tasks.Projects.Sprint
{
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetSprint;
    using AgileTracker.Common.Application.Mapping;

    public class GetSprintTaskStatusViewModel:IMapFrom<SprintTaskStatusOutputModel>
    {
        public string Title { get; set; }

        public string IsEnd { get; set; }
    }
}