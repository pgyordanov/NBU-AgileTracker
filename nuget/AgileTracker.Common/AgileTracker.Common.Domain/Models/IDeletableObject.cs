namespace AgileTracker.Common.Domain.Models
{
    public interface IDeletableObject
    {
        bool IsDeleted { get; }

        void SetAsDeleted();
    }
}
