namespace AgileTracker.Client.Startup.Models.Tasks.Projects.CreateSprint
{
    using System.Collections.Generic;

    public class CreateSprintViewModel
    {
        public int DurationWeeks { get; set; }

        public IEnumerable<int> SprintBacklog { get; set; } = default!;
    }
}
