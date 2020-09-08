namespace AgileTracker.Gateway.Authentication.Application.Identity.Features.Commands.LoginUser
{
    public class LoginUserInputModel
    {
        public LoginUserInputModel(string emailAddress, string password, bool rememberMe)
        {
            this.EmailAddress = emailAddress;
            this.Password = password;
            this.RememberMe = rememberMe;
        }

        public string EmailAddress { get; }

        public string Password { get; }

        public bool RememberMe { get; }
    }
}
