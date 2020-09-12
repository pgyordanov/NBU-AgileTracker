namespace AgileTracker.TasksService.Infrastructure.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AgileTracker.TasksService.Application.Contracts;
    using AgileTracker.TasksService.Application.Features.Queries.GetMemberProject;
    using AgileTracker.TasksService.Application.Features.Queries.GetMemberProjectGroupInvitations;
    using AgileTracker.TasksService.Application.Features.Queries.GetMemberProjectGroups;
    using AgileTracker.TasksService.Domain.Models;
    using AgileTracker.TasksService.Domain.Models.Entities;
    using AgileTracker.TasksService.Infrastructure.Persistance;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;

    internal class ProjectGroupRepository : DataRepository<ProjectGroup>, IProjectGroupRepository
    {
        private readonly IMapper _mapper;

        public ProjectGroupRepository(AgileTrackerTasksDbContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            this._mapper = mapper;
        }

        public async Task<bool> IsMember(int projectGroupId, string memberId)
        {
            return await this.All()
                            .AnyAsync(g => g.Id == projectGroupId && g.Members.Any(m => m.MemberId == memberId));
        }

        public async Task<ProjectGroup> GetById(int projectGroupId)
        {
            return await this.All()
                .Include(g => g.Members)
                .Include(g=>g.Projects)
                .FirstOrDefaultAsync(g => g.Id == projectGroupId);
        }

        public async Task<GetMemberProjectOutputModel> GetProject(int projectGroupId, int projectId)
        {
            var projectGroup = await this.All()
                                    .Include(g => g.Projects)
                                    .FirstOrDefaultAsync(g => g.Id == projectGroupId);

            var project = projectGroup.Projects.FirstOrDefault(p => p.Id == projectId);

            return this._mapper.Map<Project, GetMemberProjectOutputModel>(project);
        }

        public async Task<IEnumerable<GetMemberProjectGroupsOutputModel>> GetMemberProjectGroups(string memberId)
        {
            var projectGroupsForMember = this.All()
                .Include(g => g.Members)
                .Include(g => g.Projects)
                .Where(g => g.Members.Any(m => m.MemberId == memberId))
                .AsNoTracking();

            return await System.Threading.Tasks.Task.FromResult(this._mapper.ProjectTo<GetMemberProjectGroupsOutputModel>(projectGroupsForMember));
        }

        public async Task<IEnumerable<GetMemberProjectGroupInvitationsOutputModel>> GetMemberInvitedToProjectGroups(string memberId)
        {
            var projectGroupsForMember = this.All()
                .Include(g => g.Invitations)
                .Where(g => g.Invitations.Any(i => i.InvitedMemberId == memberId))
                .Select(g => new GetMemberProjectGroupInvitationsOutputModel(g.Id, g.GroupName));

            return await System.Threading.Tasks.Task.FromResult(projectGroupsForMember);
        }
    }
}
