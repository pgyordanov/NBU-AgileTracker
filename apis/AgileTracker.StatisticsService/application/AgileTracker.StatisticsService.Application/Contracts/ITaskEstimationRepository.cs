namespace AgileTracker.StatisticsService.Application.Contracts
{
    using AgileTracker.Common.Application.Repositories;
    using AgileTracker.StatisticsService.Domain.Models.TaskEstimation;

    public interface ITaskEstimationRepository: IRepository<TaskEstimation>
    {
        bool IsOwner(int projectGroup, string memberId);
    }
}
