namespace AgileTracker.TasksService.Domain.Exceptions
{
    using AgileTracker.Common.Domain.Exceptions;

    public class InvalidProjectGroupException: BaseDomainException
    {
        public InvalidProjectGroupException()
        {
        }

        public InvalidProjectGroupException(string message) => this.Error = message; 
    }
}
