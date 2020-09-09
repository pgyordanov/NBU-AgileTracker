namespace AgileTracker.Client.Application.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AgileTracker.Client.Application.Features.Tasks.Commands;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroup;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroups;
    using AgileTracker.Client.Application.Features.Tasks.Queries.GetUserInfo;
    using AgileTracker.Common.Application;

    public interface IGatewayService
    {
        Task<Result<GetUserInfoOutputModel>> GetUserInfo(GetUserInfoInputModel input);

        Task<Result<IEnumerable<GetProjectGroupsOutputModel>>> GetProjectGroups();

        Task<Result<CreateProjectGroupOutputModel>> CreateProjectGroup(CreateProjectGroupInputModel input);
    }
}
