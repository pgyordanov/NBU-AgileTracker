namespace AgileTracker.TasksService.Domain.Models.ValueObjects
{
    using AgileTracker.Common.Domain;
    using AgileTracker.Domain.Common.Models;
    using AgileTracker.TasksService.Domain.Exceptions;

    public class TaskDescription: ValueObject
    {
        private string _title = default!;
        private string _description = default!;
        private int _pointsEstimate;

        internal TaskDescription(string title, string description, int pointsEstimate, string assignedToMemberId)
        {
            this.Title = title;
            this.Description = description;
            this.PointsEstimate = pointsEstimate;
            this.AssignedToMemberId = assignedToMemberId;
        }

        private TaskDescription()
        {
        }

        public string Title
        {
            get => this._title;
            private set
            {
                Guard.AgainstEmptyString<InvalidProjectGroupException>(value, nameof(this.Title));
                this._title = value;
            }
        }

        public string Description
        {
            get => this._description;
            private set
            {
                Guard.AgainstEmptyString<InvalidProjectGroupException>(value, nameof(this.Description));
                this._description = value;
            }
        }

        public int PointsEstimate
        {
            get => this._pointsEstimate;
            private set
            {
                Guard.AgainstOutOfRange<InvalidProjectGroupException>(value, 0, 10, nameof(this.PointsEstimate));
                this._pointsEstimate = value;
            }
        }

        public string AssignedToMemberId { get; private set; } = default!;
    }
}
