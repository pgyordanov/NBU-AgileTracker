namespace AgileTracker.Client.Startup.Models.Tasks.Projects.UpdateTaskEstimation
{
    using System;

    public class UpdateTaskEstimationViewModel
    {
        public int TaskId { get; set; }

        public DateTime EstimatedToFinishOn { get; set; }
    }
}
