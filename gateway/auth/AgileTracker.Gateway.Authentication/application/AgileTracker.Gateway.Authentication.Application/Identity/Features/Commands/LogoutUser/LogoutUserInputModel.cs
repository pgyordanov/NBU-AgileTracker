namespace AgileTracker.Gateway.Authentication.Application.Identity.Features.Commands.LogoutUser
{
    public class LogoutUserInputModel
    {
        public LogoutUserInputModel(string logoutId)
        {
            this.LogoutId = logoutId;
        }

        public string LogoutId { get; }
    }
}
