﻿namespace AgileTracker.TasksService.Domain.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AgileTracker.Common.Domain;
    using AgileTracker.Domain.Common.Models;
    using AgileTracker.TasksService.Domain.Exceptions;
    using AgileTracker.TasksService.Domain.Models.ValueObjects;

    public class Project: Entity<int>
    {
        private string _title = default!;
        private HashSet<Task> _backlog;
        private HashSet<Sprint> _sprints;

        internal Project(string title)
        {
            this.Title = title;
            this._backlog = new HashSet<Task>();
            this._sprints = new HashSet<Sprint>();
        }

        private Project()
        {
            this._backlog = new HashSet<Task>();
            this._sprints = new HashSet<Sprint>();
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

        public IReadOnlyCollection<Task> Backlog
            => this._backlog.ToList().AsReadOnly();

        public IReadOnlyCollection<Sprint> Sprints
            => this._sprints.ToList().AsReadOnly();

        public void AddToBacklog(TaskDescription taskDescription, DateTime startsOn)
        {
            this._backlog.Add(new Task(taskDescription, startsOn));
        }

        public void RemoveFromBacklog(int taskId)
        {
            var task = this._backlog.Where(t => t.Id == taskId).FirstOrDefault();

            Guard.Against<InvalidProjectGroupException>(task, null!, nameof(task));
            
            this._backlog.Remove(task);
        }

        public void UpdateTask(int taskId, TaskDescription description)
        {
            var task = this._backlog.FirstOrDefault(t => t.Id == taskId);

            Guard.Against<InvalidProjectGroupException>(task, null!, nameof(task));

            task.UpdateTask(description);
        }

        public void CreateSprint(IEnumerable<int> taskIds, DateTime startsOn, int durationWeeks)
        {
            var backlogTasks = this._backlog.Where(bt => taskIds.Contains(bt.Id)).ToList();
            this._sprints.Add(new Sprint(backlogTasks, startsOn, durationWeeks));
        }

        public void FinishSprint(int sprintId)
        {
            var sprint = this._sprints.Where(s => s.Id == sprintId).FirstOrDefault();

            Guard.Against<InvalidProjectGroupException>(sprint, null!, nameof(sprint));

            sprint.FinishSprint();
        }

        public void ChangeSprintTaskStatus(int sprintId, int taskId, TaskStatus status)
        {
            var sprint = this._sprints.FirstOrDefault(s => s.Id == sprintId);

            Guard.Against<InvalidProjectGroupException>(sprint, null!, nameof(sprint));

            var task = this._backlog.FirstOrDefault(s => s.Id == taskId);

            Guard.Against<InvalidProjectGroupException>(task, null!, nameof(sprint));

            sprint.ChangeTaskStatus(task, status);
        }
    }
}