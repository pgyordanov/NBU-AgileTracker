﻿namespace AgileTracker.Client.Application.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Features.Tasks.Commands.CreateProjectGroup;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroups;
    using AgileTracker.Client.Application.Features.Identity.GetUserInfo;
    using AgileTracker.Common.Application;
    using AgileTracker.Client.Application.Features.Identity.IsEmailRegistered;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroupInvitations;
    using AgileTracker.Client.Application.Features.Tasks.Commands.AcceptProjectGroupInvitation;
    using AgileTracker.Client.Application.Features.Tasks.Commands.CreateProject;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProject;
    using AgileTracker.Client.Application.Features.Tasks.Commands.AddToProjectBacklog;
    using AgileTracker.Client.Application.Features.Tasks.Commands.RemoveFromProjectBacklog;
    using AgileTracker.Client.Application.Features.Tasks.Commands.UpdateBacklogTask;

    public interface IGatewayService
    {
        Task<Result<GetUserInfoOutputModel>> GetUserInfo(GetUserInfoInputModel input);

        Task<Result<IsEmailRegisteredOutputModel>> IsEmailRegistered(IsEmailRegisteredInputModel input);

        Task<Result<IEnumerable<GetProjectGroupsOutputModel>>> GetProjectGroups();

        Task<Result<CreateProjectGroupOutputModel>> CreateProjectGroup(CreateProjectGroupInputModel input);

        Task<Result<IEnumerable<GetProjectGroupInvitationsOutputModel>>> GetProjectGroupInvitations();

        Task<Result> InviteMemberToProjectGroup(int projectGroupId, string memberId);

        Task<Result> AcceptProjectGroupInvitation(AcceptProjectGroupInvitationInputModel input);

        Task<Result<CreateProjectOutputModel>> CreateProject(CreateProjectInputModel input);

        Task<Result<GetProjectOutputModel>> GetProject(GetProjectInputModel input);

        Task<Result> AddToProjectBacklog(AddToProjectBacklogInputModel input);

        Task<Result> RemoveFromProjectBacklog(RemoveFromProjectBacklogInputModel input);

        Task<Result> UpdateBacklogTask(UpdateBacklogTaskInputModel input);
    }
}
