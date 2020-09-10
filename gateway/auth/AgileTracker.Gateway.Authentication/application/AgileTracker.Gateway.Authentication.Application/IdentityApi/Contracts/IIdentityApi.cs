namespace AgileTracker.Gateway.Authentication.Application.IdentityApi.Contracts
{
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Gateway.Authentication.Application.IdentityApi.Features.Queries.GetUsersInformation;
    using AgileTracker.Gateway.Authentication.Application.IdentityApi.Features.Queries.IsEmailRegistered;

    public interface IIdentityApi
    {
        Task<Result<GetUsersInformationOutputModel>> GetUsersInformation(GetUsersInformationInputModel input);

        Task<Result<IsEmailRegisteredOutputModel>> IsEmailRegistered(IsEmailRegisteredInputModel input);
    }
}
