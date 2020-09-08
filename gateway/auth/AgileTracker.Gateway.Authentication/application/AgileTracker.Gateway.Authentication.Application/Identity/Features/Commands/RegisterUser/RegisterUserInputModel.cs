namespace AgileTracker.Gateway.Authentication.Application.Identity.Features.Commands.RegisterUser
{
    public class RegisterUserInputModel
    {
        public RegisterUserInputModel(string firstname, string lastname, string emailAddress, string password)
        {
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.EmailAddress = emailAddress;
            this.Password = password;
        }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }
    }
}
