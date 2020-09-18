namespace AgileTracker.StatisticsService.Application.Contracts
{
    using System.Threading.Tasks;

    using AgileTracker.Common.Application.Repositories;
    using AgileTracker.StatisticsService.Domain.Models.TaskEstimation;

    public interface ITaskEstimationRepository: IRepository<TaskEstimation>
    {
        Task<bool> IsOwner(int projectGroupId, string memberId);

        Task AddProjectGroupOwnership(int projectGroupId, string ownerId);
    }
}
