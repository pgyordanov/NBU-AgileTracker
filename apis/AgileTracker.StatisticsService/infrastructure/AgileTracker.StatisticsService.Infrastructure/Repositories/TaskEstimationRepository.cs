namespace AgileTracker.StatisticsService.Infrastructure.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AgileTracker.StatisticsService.Application.Contracts;
    using AgileTracker.StatisticsService.Application.Features.Queries.GetTaskEstimations;
    using AgileTracker.StatisticsService.Domain.Models.TaskEstimation;
    using AgileTracker.StatisticsService.Domain.Specifications;
    using AgileTracker.StatisticsService.Infrastructure.Persistance;
    using AgileTracker.StatisticsService.Infrastructure.Persistance.Models;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;

    internal class TaskEstimationRepository : DataRepository<TaskEstimation>, ITaskEstimationRepository
    {
        private readonly IMapper _mapper;

        public TaskEstimationRepository(AgileTrackerStatisticsDbContext data, IMapper mapper)
            : base(data)
        {
            this._mapper = mapper;
        }

        public async Task<TaskEstimation> GetById(int taskEstimationId)
        {
            return await this.All().FirstOrDefaultAsync(t => t.Id == taskEstimationId);
        }

        public async Task<TaskEstimation> GetByKeys(int projectGroupId, int projectId, int taskId)
        {
            return await this.All().FirstOrDefaultAsync(t => t.ProjectGroupId == projectGroupId && t.ProjectId == projectId && t.TaskId == taskId);
        }

        public async Task<bool> IsOwner(int projectGroupId, string memberId)
            => await this.Data.ProjectGroupOwnerships.AnyAsync(po => po.ExternalProjectGroupId == projectGroupId && po.OwnerId == memberId);

        public async Task AddProjectGroupOwnership(int projectGroupId, string ownerId)
        {
            this.Data.ProjectGroupOwnerships.Add(new ProjectGroupOwnership(projectGroupId, ownerId));

            await this.Data.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetTaskEstimationsOutputModel>> GetTaskEstimations(Specification<TaskEstimation> specification)
        {
            return await this._mapper.ProjectTo<GetTaskEstimationsOutputModel>(this.All().Where(specification)).ToListAsync();
        }
    }
}
