namespace AgileTracker.TasksService.Domain.Models.Entities
{
    using System;

    using AgileTracker.Domain.Common.Models;
    using AgileTracker.TasksService.Domain.Models.ValueObjects;

    public class Task : Entity<int>
    {
        internal Task(TaskData description, DateTime startsOn)
        {
            this.Data = description;
            this.StartedOn = startsOn.ToUniversalTime();

            this.Status = new TaskStatus("New", false);
        }

        private Task()
        {
            this.Data = default!;
            this.Status = default!;
        }

        public TaskData Data { get; private set; }

        public TaskStatus Status { get; private set; }

        public DateTime StartedOn { get; private set; }

        public DateTime FinishedOn { get; private set; }

        public bool IsFinished { get; private set; }

        public void UpdateTask(TaskData description)
        {
            this.Data = description;
        }

        public void ChangeStatus(TaskStatus taskStatus)
        {
            this.Status = taskStatus;

            if (this.Status.IsEnd)
            {
                this.FinishedOn = DateTime.UtcNow;
                this.IsFinished = true;
            }
            else
            {
                this.FinishedOn = default!;
                this.IsFinished = false;
            }
        }
    }
}
