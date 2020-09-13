namespace AgileTracker.TasksService.Application.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application.Repositories;
    using AgileTracker.TasksService.Application.Features.Queries.GetMemberProject;
    using AgileTracker.TasksService.Application.Features.Queries.GetMemberProjectGroupInvitations;
    using AgileTracker.TasksService.Application.Features.Queries.GetMemberProjectGroups;
    using AgileTracker.TasksService.Application.Features.Queries.GetMemberSprint;
    using AgileTracker.TasksService.Domain.Models;

    public interface IProjectGroupRepository : IRepository<ProjectGroup>
    {
        Task<bool> IsMember(int projectGroupId, string memberId);

        Task<ProjectGroup> GetById(int projectGroupId);

        Task<Features.Queries.GetMemberProject.GetMemberProjectOutputModel> GetProject(int projectGroupId, int projectId);

        Task<Features.Queries.GetMemberSprint.GetMemberSprintOutputModel> GetSprint(int projectGroupId, int projectId, int sprintId);

        Task<IEnumerable<GetMemberProjectGroupsOutputModel>> GetMemberProjectGroups(string memberId);

        Task<IEnumerable<GetMemberProjectGroupInvitationsOutputModel>> GetMemberInvitedToProjectGroups(string memberId);
    }
}
