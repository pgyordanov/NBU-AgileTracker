namespace AgileTracker.Common.Domain.Exceptions
{
    using System;

    public abstract class BaseDomainException : Exception
    {
        private string? _error;

        public string Error
        {
            get => _error ?? base.Message;
            set => this._error = value;
        }
    }
}
