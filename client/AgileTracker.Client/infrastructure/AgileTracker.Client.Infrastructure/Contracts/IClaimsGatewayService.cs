namespace AgileTracker.Client.Infrastructure.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AgileTracker.Client.Application.Features.Tasks.Queries.GetProjectGroups;
    using AgileTracker.Common.Application;

    internal interface IClaimsGatewayService
    {
        Task<Result<IEnumerable<GetProjectGroupsOutputModel>>> GetProjectGroups(string accessToken);
    }
}
