namespace AgileTracker.StatisticsService.Application.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application.Repositories;
    using AgileTracker.StatisticsService.Application.Features.Queries.GetTaskEstimations;
    using AgileTracker.StatisticsService.Domain.Models.TaskEstimation;
    using AgileTracker.StatisticsService.Domain.Specifications;

    public interface ITaskEstimationRepository: IRepository<TaskEstimation>
    {
        Task<TaskEstimation> GetById(int taskEstimationId);

        Task<TaskEstimation> GetByKeys(int projectGroupId, int projectId, int taskId);

        Task<IEnumerable<GetTaskEstimationsOutputModel>> GetTaskEstimations(Specification<TaskEstimation> specification);

        Task<bool> IsOwner(int projectGroupId, string memberId);

        Task AddProjectGroupOwnership(int projectGroupId, string ownerId);
    }
}
