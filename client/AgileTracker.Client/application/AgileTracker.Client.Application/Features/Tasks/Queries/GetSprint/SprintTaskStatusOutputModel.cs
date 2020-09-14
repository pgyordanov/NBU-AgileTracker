namespace AgileTracker.Client.Application.Features.Tasks.Queries.GetSprint
{
    public class SprintTaskStatusOutputModel
    {
        public string Title { get; set; } = default!;

        public string IsEnd { get; set; } = default!;
    }
}