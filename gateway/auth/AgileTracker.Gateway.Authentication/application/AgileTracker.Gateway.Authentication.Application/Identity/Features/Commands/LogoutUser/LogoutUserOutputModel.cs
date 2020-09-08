namespace AgileTracker.Gateway.Authentication.Application.Identity.Features.Commands.LogoutUser
{
    public class LogoutUserOutputModel
    {
        public string PostLogoutRedirectUri { get; set; } = default!;
    }
}
