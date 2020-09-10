namespace AgileTracker.Gateway.Authentication.Application.IdentityApi.Features.Queries.IsEmailRegistered
{
    public class IsEmailRegisteredOutputModel
    {
        public IsEmailRegisteredOutputModel(string userId, bool isRegistered)
        {
            this.UserId = userId;
            this.IsEmailRegistered = isRegistered;
        }

        public string UserId { get; }

        public bool IsEmailRegistered { get; }
    }
}
