namespace AgileTracker.Client.Infrastructure.Contracts
{
    public interface ITokenService
    {
        string IdToken { get; }

        string AccessToken { get; }
    }
}
