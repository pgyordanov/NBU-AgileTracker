namespace AgileTracker.Gateway.Authentication.Infrastructure.Identity
{
    using System.Linq;
    using System.Threading.Tasks;

    using AgileTracker.Common.Application;
    using AgileTracker.Gateway.Authentication.Application.Identity.Contracts;
    using AgileTracker.Gateway.Authentication.Application.Identity.Features.Commands.LoginUser;
    using AgileTracker.Gateway.Authentication.Application.Identity.Features.Commands.LogoutUser;
    using AgileTracker.Gateway.Authentication.Application.Identity.Features.Commands.RegisterUser;
    using AgileTracker.Gateway.Authentication.Infrastructure.Identity.Persistance.Identity.Models;

    using IdentityServer4.Services;

    using Microsoft.AspNetCore.Identity;

    public class IdentityService : IIdentity
    {
        private const string UserNotFoundErrorMessage = "User with email address not found";
        private const string SignInErrorMessage = "Cannot sign in at this time";

        private readonly UserManager<AgileTrackerUser> _userManager;
        private readonly SignInManager<AgileTrackerUser> _signInManager;
        private readonly IIdentityServerInteractionService _identityServerInteractionService;


        public IdentityService(
            UserManager<AgileTrackerUser> userManager,
            SignInManager<AgileTrackerUser> signInManager,
            IIdentityServerInteractionService identityServerInteractionService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._identityServerInteractionService = identityServerInteractionService;
        }

        public async Task<Result> LoginUser(LoginUserInputModel model)
        {
            var user = await this._userManager.FindByEmailAsync(model.EmailAddress);

            if (user == null)
            {
                return UserNotFoundErrorMessage;
            }

            var result = await this._signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {
                return SignInErrorMessage;
            }

            return true;
        }

        public async Task<Result> RegisterUser(RegisterUserInputModel model)
        {
            var user = new AgileTrackerUser
            {
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                UserName = model.EmailAddress,
                Email = model.EmailAddress
            };

            var createResult = await this._userManager.CreateAsync(user, model.Password);

            if (!createResult.Succeeded)
            {
                return createResult.Errors.Select(e => e.Description).ToList();
            }

            await this._signInManager.SignInAsync(user, false);

            return true;
        }

        public async Task<Result<LogoutUserOutputModel>> LogoutUser(LogoutUserInputModel model)
        {
            await this._signInManager.SignOutAsync();

            var logoutRequest = await this._identityServerInteractionService.GetLogoutContextAsync(model.LogoutId);

            return Result<LogoutUserOutputModel>
                .SuccessWith(
                new LogoutUserOutputModel
                {
                    PostLogoutRedirectUri = logoutRequest.PostLogoutRedirectUri
                });
        }
    }
}
