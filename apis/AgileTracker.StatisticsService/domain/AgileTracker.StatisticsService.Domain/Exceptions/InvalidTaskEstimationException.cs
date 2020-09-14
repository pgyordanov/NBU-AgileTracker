namespace AgileTracker.StatisticsService.Domain.Exceptions
{
    using AgileTracker.Common.Domain.Exceptions;

    public class InvalidTaskEstimationException: BaseDomainException
    {
        public InvalidTaskEstimationException()
        {
        }

        public InvalidTaskEstimationException(string message)
            => this.Error = message;
    }
}
