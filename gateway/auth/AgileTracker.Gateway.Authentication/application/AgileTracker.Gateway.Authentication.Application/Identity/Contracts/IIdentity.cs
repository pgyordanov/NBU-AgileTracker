namespace AgileTracker.Gateway.Authentication.Application.Identity.Contracts
{
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Gateway.Authentication.Application.Identity.Features.Commands.LoginUser;
    using AgileTracker.Gateway.Authentication.Application.Identity.Features.Commands.LogoutUser;
    using AgileTracker.Gateway.Authentication.Application.Identity.Features.Commands.RegisterUser;

    public interface IIdentity
    {
        Task<Result> LoginUser(LoginUserInputModel model);

        Task<Result> RegisterUser(RegisterUserInputModel model);

        Task<Result<LogoutUserOutputModel>> LogoutUser(LogoutUserInputModel model);
    }
}
