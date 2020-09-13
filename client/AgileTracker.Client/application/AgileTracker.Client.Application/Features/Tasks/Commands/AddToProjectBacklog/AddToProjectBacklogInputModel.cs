namespace AgileTracker.Client.Application.Features.Tasks.Commands.AddToProjectBacklog
{
    using System;

    public class AddToProjectBacklogInputModel
    {
        public AddToProjectBacklogInputModel(
            int projectGroupId, 
            int projectId, 
            string title,
            string description,
            int pointsEstimate,
            string assignedToMemberId,
            DateTime startsOn)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.Title = title;
            this.Description = description;
            this.PointsEstimate = pointsEstimate;
            this.AssignedToMemberId = assignedToMemberId;
            this.StartsOn = startsOn;
        }

        public int ProjectGroupId { get; }

        public int ProjectId { get; }

        public string Title { get; }

        public string Description { get; }

        public int PointsEstimate { get; }

        public string AssignedToMemberId { get; }

        public DateTime StartsOn { get; }
    }
}
