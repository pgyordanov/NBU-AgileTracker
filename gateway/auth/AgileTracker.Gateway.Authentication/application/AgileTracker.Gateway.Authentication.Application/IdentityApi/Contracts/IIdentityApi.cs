namespace AgileTracker.Gateway.Authentication.Application.IdentityApi.Contracts
{
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Gateway.Authentication.Application.IdentityApi.Features.Queries.GetUsersInformation;

    public interface IIdentityApi
    {
        Task<Result<GetUsersInformationOutputModel>> GetUsersInformation(GetUsersInformationInputModel input);
    }
}
