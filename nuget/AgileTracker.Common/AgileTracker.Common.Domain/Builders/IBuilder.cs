namespace AgileTracker.Common.Domain.Builders
{
    using AgileTracker.Domain.Common.Models;

    public interface IBuilder<out TValueObject>
        where TValueObject: ValueObject
    {
        TValueObject Build();
    }
}
