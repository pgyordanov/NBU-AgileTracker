namespace AgileTracker.StatisticsService.Domain.Factories
{
    using System;

    using AgileTracker.StatisticsService.Domain.Models.TaskEstimation;
    using AgileTracker.StatisticsService.Domain.Repositories;

    public class TaskEstimationFactory : ITaskEstimationFactory
    {
        private int _projectGroupId;
        private int _projectId;
        private int _taskId;
        private string _estimatedByMemberId = default!;
        private DateTime _estimatedFinishTime;
        private DateTime _startedOn;

        public ITaskEstimationFactory WithKeys(int projectGroupId, int projectId, int taskId)
        {
            this._projectGroupId = projectGroupId;
            this._projectId = projectId;
            this._taskId = taskId;

            return this;
        }

        public ITaskEstimationFactory WithEstimatorId(string estimatedByMemberId)
        {
            this._estimatedByMemberId = estimatedByMemberId;

            return this;
        }

        public ITaskEstimationFactory WithStartTime(DateTime startedOn)
        {
            this._startedOn = startedOn;

            return this;
        }

        public ITaskEstimationFactory WithEstimatedFinishTime(DateTime estimatedFinishTime)
        {
            this._estimatedFinishTime = estimatedFinishTime;

            return this;
        }

        public TaskEstimation Build()
        {
            return new TaskEstimation(
                this._projectGroupId,
                this._projectId, 
                this._taskId,
                this._estimatedByMemberId,
                this._startedOn, 
                this._estimatedFinishTime);
        }
    }
}
