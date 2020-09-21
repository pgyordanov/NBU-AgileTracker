namespace AgileTracker.Client.Startup.Models.Tasks.Projects.SaveTaskEstimation
{
    using System;

    public class CreateTaskEstimationViewModel
    {
        public int TaskId { get; set; }

        public DateTime StartedOn { get; set; }

        public DateTime EstimatedToFinishOn { get; set; }
    }
}
