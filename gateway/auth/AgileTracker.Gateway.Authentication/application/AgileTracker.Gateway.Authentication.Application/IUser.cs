namespace AgileTracker.Gateway.Authentication.Application
{
    public interface IUser
    {
        string Id { get; }

        string UserName { get; }

        string Firstname { get; }

        string Lastname { get; }
    }
}
