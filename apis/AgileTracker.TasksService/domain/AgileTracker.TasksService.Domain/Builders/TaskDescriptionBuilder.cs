namespace AgileTracker.TasksService.Domain.Builders
{
    using AgileTracker.TasksService.Domain.Models.ValueObjects;

    public class TaskDescriptionBuilder : ITaskDescriptionBuilder
    {
        private string _title = default!;
        private string _description = default!;
        private int _pointsEstimate;
        private string _assignedToMemberId = default!;

        public ITaskDescriptionBuilder WithTitle(string title)
        {
            this._title = title;
            return this;
        }

        public ITaskDescriptionBuilder WithDescription(string description)
        {
            this._description = description;
            return this;
        }

        public ITaskDescriptionBuilder WithPointsEstimation(int pointsEstimation)
        {
            this._pointsEstimate = pointsEstimation;
            return this;
        }

        public ITaskDescriptionBuilder WithAssignedToMemberId(string assignedToMemberId)
        {
            this._assignedToMemberId = assignedToMemberId;
            return this;
        }

        public TaskData Build()
        {
            var description = new TaskData(this._title, this._description, this._pointsEstimate, this._assignedToMemberId);

            this._title = default!;
            this._description = default!;
            this._pointsEstimate = default!;
            this._assignedToMemberId = default!;

            return description;
        }
    }
}
