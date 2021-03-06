﻿namespace AgileTracker.StatisticsService.Domain.Models.TaskEstimation
{
    using System;

    using AgileTracker.Common.Domain;
    using AgileTracker.Domain.Common.Models;
    using AgileTracker.StatisticsService.Domain.Exceptions;

    public class TaskEstimation : Entity<int>, IAggregateRoot
    {
        private DateTime _startedOn;
        private DateTime _estimatedToFinishOn;
        private DateTime _actuallyFinishedOn;

        internal TaskEstimation(
            int projectGroupId,
            int projectId,
            int taskId,
            string estimatedByMemberId,
            DateTime startedOn,
            DateTime estimatedToFinishOn)
        {
            this.ProjectGroupId = projectGroupId;
            this.ProjectId = projectId;
            this.TaskId = taskId;
            this.EstimatedByMemberId = estimatedByMemberId;
            this.StartedOn = startedOn;
            this.EstimatedToFinishOn = estimatedToFinishOn;
        }

        private TaskEstimation()
        {
        }

        public int TaskId { get; private set; }

        public int ProjectGroupId { get; private set; }

        public int ProjectId { get; private set; }

        public string EstimatedByMemberId { get; private set; } = default!;

        public DateTime StartedOn
        {
            get => this._startedOn;
            private set
            {
                Guard.Against<InvalidTaskEstimationException>(value, null!, nameof(this.StartedOn));
                this._startedOn = value;
            }
        }

        public DateTime EstimatedToFinishOn
        {
            get => this._estimatedToFinishOn;
            private set
            {
                Guard.Against<InvalidTaskEstimationException>(value, null!, nameof(this.EstimatedToFinishOn));
                this._estimatedToFinishOn = value;
            }
        }

        public DateTime ActuallyFinishedOn
        {
            get => this._actuallyFinishedOn;
            private set
            {
                Guard.Against<InvalidTaskEstimationException>(value, null!, nameof(this.ActuallyFinishedOn));
                this._actuallyFinishedOn = value;
            }
        }

        public void SetActualFinishedOnTime(DateTime finishedOn)
            => this.ActuallyFinishedOn = finishedOn;

        public void SetEstimatedToFinishOnTime(DateTime estimatedToFinishOn)
            => this.EstimatedToFinishOn = estimatedToFinishOn;
    }
}
