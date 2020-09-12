namespace AgileTracker.Client.Startup.Models.Tasks.Projects.AddToBacklog
{
    public class AddTaskViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int PointsEstimate { get; set; }

        public string AssignedToMemberId { get; set; }
    }
}
