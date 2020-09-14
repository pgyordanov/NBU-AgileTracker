namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetProject
{
    public class ProjectTaskStatusOutputModel
    {
        public string Title { get; set; } = default!;

        public string IsEnd { get; set; } = default!;
    }
}