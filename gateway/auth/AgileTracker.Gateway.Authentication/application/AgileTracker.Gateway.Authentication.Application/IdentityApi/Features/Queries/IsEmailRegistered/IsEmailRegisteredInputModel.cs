namespace AgileTracker.Gateway.Authentication.Application.IdentityApi.Features.Queries.IsEmailRegistered
{
    public class IsEmailRegisteredInputModel
    {
        public IsEmailRegisteredInputModel(string userEmail)
        {
            this.UserEmail = userEmail;
        }

        public string UserEmail { get; }
    }
}
