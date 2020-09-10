namespace AgileTracker.Client.Application.Features.Identity.IsEmailRegistered
{
    public class IsEmailRegisteredInputModel
    {
        public IsEmailRegisteredInputModel(string userEmail)
        {
            this.UserEmail = userEmail;
        }

        public string UserEmail { get; private set; }
    }
}
