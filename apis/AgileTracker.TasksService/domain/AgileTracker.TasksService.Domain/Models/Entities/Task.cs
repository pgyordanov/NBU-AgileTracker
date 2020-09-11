namespace AgileTracker.TasksService.Domain.Models.Entities
{
    using System;

    using AgileTracker.Domain.Common.Models;
    using AgileTracker.TasksService.Domain.Models.ValueObjects;

    public class Task : Entity<int>
    {
        internal Task(TaskDescription description, DateTime startsOn)
        {
            this.Description = description;
            this.StartedOn = startsOn.ToUniversalTime();
        }

        private Task()
        {
        }

        public TaskDescription Description { get; private set; } = default!;

        public TaskStatus Status { get; private set; } = default!;

        public DateTime StartedOn { get; private set; }

        public DateTime FinishedOn { get; private set; }

        public bool IsFinished { get; private set; }

        public void UpdateTask(TaskDescription description)
        {
            this.Description = description;
        }

        public void ChangeStatus(TaskStatus taskStatus)
        {
            this.Status = taskStatus;

            if (this.Status.IsEnd)
            {
                this.FinishedOn = DateTime.UtcNow;
                this.IsFinished = true;
            }
        }
    }
}
