﻿namespace AgileTracker.Client.Application.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Features.Tasks.Commands.CreateProjectGroup;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroups;
    using AgileTracker.Client.Application.Features.Identity.GetUserInfo;
    using AgileTracker.Common.Application;
    using AgileTracker.Client.Application.Features.Identity.IsEmailRegistered;

    public interface IGatewayService
    {
        Task<Result<GetUserInfoOutputModel>> GetUserInfo(GetUserInfoInputModel input);

        Task<Result<IsEmailRegisteredOutputModel>> IsEmailRegistered(IsEmailRegisteredInputModel input);

        Task<Result<IEnumerable<GetProjectGroupsOutputModel>>> GetProjectGroups();

        Task<Result<CreateProjectGroupOutputModel>> CreateProjectGroup(CreateProjectGroupInputModel input);

        Task<Result> InviteMemberToProjectGroup(int projectGroupId, string memberId);
    }
}
