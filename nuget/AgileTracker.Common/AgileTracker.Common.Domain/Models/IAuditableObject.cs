namespace AgileTracker.Common.Domain.Models
{
    using System;

    public interface IAuditableObject
    {
        DateTime CreatedOn { get; }

        DateTime LastModifiedOn { get; }

        void SetModifiedTime(DateTime modifiedOn);
    }
}
