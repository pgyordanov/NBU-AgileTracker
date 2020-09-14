namespace AgileTracker.Client.Startup.Models.Tasks.Projects.AddToBacklog
{
    public class AddTaskViewModel
    {
        public string Title { get; set; } = default!;

        public string Description { get; set; } = default!;

        public int PointsEstimate { get; set; }

        public string AssignedToMemberId { get; set; } = default!;
    }
}
