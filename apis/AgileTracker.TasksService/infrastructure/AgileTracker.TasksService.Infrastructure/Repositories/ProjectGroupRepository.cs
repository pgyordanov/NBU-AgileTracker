namespace AgileTracker.TasksService.Infrastructure.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AgileTracker.TasksService.Application.Contracts;
    using AgileTracker.TasksService.Application.Features.Queries.GetMemberProject;
    using AgileTracker.TasksService.Application.Features.Queries.GetMemberProjectGroupInvitations;
    using AgileTracker.TasksService.Application.Features.Queries.GetMemberProjectGroups;
    using AgileTracker.TasksService.Application.Features.Queries.GetMemberSprint;
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
                .Include(g => g.Projects).ThenInclude(p => p.Backlog)
                .Include(g => g.Projects).ThenInclude(p => p.Sprints)
                .FirstOrDefaultAsync(g => g.Id == projectGroupId);
        }

        public async Task<Application.Features.Queries.GetMemberProject.GetMemberProjectOutputModel> GetProject(int projectGroupId, int projectId)
        {
            var projectGroup = await this.All()
                                        .Include(g => g.Members)
                                        .Include(g => g.Projects).ThenInclude(p => p.Backlog)
                                        .Include(g => g.Projects).ThenInclude(p => p.Sprints)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(g => g.Id == projectGroupId);

            var project = projectGroup.Projects.FirstOrDefault(p => p.Id == projectId);

            var mappedProject = this._mapper.Map<Project, Application.Features.Queries.GetMemberProject.GetMemberProjectOutputModel>(project);

            mappedProject.AddMembers(this._mapper.ProjectTo<ProjectMemberOutputModel>(projectGroup.Members.AsQueryable()));

            return mappedProject;
        }


        public async Task<GetMemberSprintOutputModel> GetSprint(int projectGroupId, int projectId, int sprintId)
        {
            var projectGroup = await this.All()
                                        .Include(g => g.Projects).ThenInclude(p => p.Sprints).ThenInclude(s=>s.SprintBacklog)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(g => g.Id == projectGroupId);

            var sprint = projectGroup.Projects.FirstOrDefault(p => p.Id == projectId).Sprints.FirstOrDefault(s => s.Id == sprintId);

            return this._mapper.Map<Sprint, GetMemberSprintOutputModel>(sprint);
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
