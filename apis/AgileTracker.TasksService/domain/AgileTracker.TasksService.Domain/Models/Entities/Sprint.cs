namespace AgileTracker.TasksService.Domain.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AgileTracker.Common.Domain;
    using AgileTracker.Domain.Common.Models;
    using AgileTracker.TasksService.Domain.Exceptions;
    using AgileTracker.TasksService.Domain.Models.ValueObjects;

    public class Sprint : Entity<int>
    {
        private HashSet<Task> _sprintBacklog;
        private HashSet<TaskStatus> _taskStatuses;
        private int _durationWeeks;

        internal Sprint(IEnumerable<Task> sprintBacklog, DateTime startsOn, int durationWeeks)
        {
            this._sprintBacklog = sprintBacklog.ToHashSet();
            this._taskStatuses = new HashSet<TaskStatus>(){
                new TaskStatus("New", false),
                new TaskStatus("Pending", false),
                new TaskStatus("InProgress", false),
                new TaskStatus("Done", true)
            };

            this.DurationWeeks = durationWeeks;
            this.StartedOn = startsOn.ToUniversalTime();
        }

        private Sprint()
        {
            this._sprintBacklog = new HashSet<Task>();
            this._taskStatuses = new HashSet<TaskStatus>();
        }

        public IReadOnlyCollection<Task> SprintBacklog
            => this._sprintBacklog.ToList().AsReadOnly();

        public IReadOnlyCollection<TaskStatus> TaskStatuses
            => this._taskStatuses.ToList().AsReadOnly();

        public int DurationWeeks
        {
            get => this._durationWeeks;
            private set
            {
                Guard.AgainstOutOfRange<InvalidProjectGroupException>(value, 1, 4, nameof(value));
                this._durationWeeks = value;
            }
        }

        public DateTime StartedOn { get; private set; }

        public DateTime FinishedOn { get; private set; }

        public bool IsFinished { get; private set; }

        public void AddTask(Task task)
            => this._sprintBacklog.Add(task);

        public void RemoveTask(Task task)
            => this._sprintBacklog.Remove(task);

        public void ChangeTaskStatus(Task task, TaskStatus status)
        {
            if (this._taskStatuses.Contains(status))
            {
                task.ChangeStatus(status);
            }
        }

        public void FinishSprint()
        {
            this.FinishedOn = DateTime.UtcNow;
            this.IsFinished = true;
        }
    }
}
