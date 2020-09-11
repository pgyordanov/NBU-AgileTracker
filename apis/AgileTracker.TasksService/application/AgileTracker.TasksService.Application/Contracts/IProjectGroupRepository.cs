namespace AgileTracker.TasksService.Application.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application.Repositories;
    using AgileTracker.TasksService.Application.Features.Queries.GetMemberProjectGroupInvitations;
    using AgileTracker.TasksService.Application.Features.Queries.GetMemberProjectGroups;
    using AgileTracker.TasksService.Domain.Models;

    public interface IProjectGroupRepository : IRepository<ProjectGroup>
    {
        Task<ProjectGroup> GetById(int projectGroupId);

        Task<IEnumerable<GetMemberProjectGroupsOutputModel>> GetMemberProjectGroups(string memberId);

        Task<IEnumerable<GetMemberProjectGroupInvitationsOutputModel>> GetMemberInvitedToProjectGroups(string memberId);
    }
}
