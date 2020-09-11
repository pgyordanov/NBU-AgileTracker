namespace AgileTracker.TasksService.Domain.Models.ValueObjects
{
    using AgileTracker.Common.Domain;
    using AgileTracker.Domain.Common.Models;
    using AgileTracker.TasksService.Domain.Exceptions;

    public class TaskStatus: ValueObject
    {
        private string _title = default!;

        internal TaskStatus(string title, bool isEnd)
        {
            this.Title = title;
            this.IsEnd = isEnd;
        }

        private TaskStatus()
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

        public bool IsEnd { get; private set; }
    }
}
