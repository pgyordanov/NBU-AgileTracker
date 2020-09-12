namespace AgileTracker.TasksService.Application.Features.Commands.AddToProjectBacklog
{
    using System;

    public class AddToProjectBacklogInputModel
    {
        public AddToProjectBacklogInputModel(
            int projectGroupId, 
            int projectId,
            string memberId,
            string title, 
            string description, 
            int pointsEstimate, 
            DateTime startsOn)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.MemberId = memberId;
            this.Title = title;
            this.Description = description;
            this.PointsEstimate = pointsEstimate;
            this.StartsOn = startsOn;
        }

        public int ProjectGroupId { get; }

        public int ProjectId { get; }

        public string MemberId { get; }

        public string Title { get; }

        public string Description { get; }

        public int PointsEstimate { get; }

        public DateTime StartsOn { get; }
    }
}
