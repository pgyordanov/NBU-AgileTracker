namespace AgileTracker.StatisticsService.Domain.Repositories
{
    using System;

    using AgileTracker.Common.Domain.Factories;
    using AgileTracker.StatisticsService.Domain.Models.TaskEstimation;

    public interface ITaskEstimationFactory: IFactory<TaskEstimation>
    {
        ITaskEstimationFactory WithKeys(int projectGroupId, int projectId, int taskId);

        ITaskEstimationFactory WithStartTime(DateTime startedOn);

        ITaskEstimationFactory WithEstimatedFinishTime(DateTime estimatedFinishTime);
    }
}
