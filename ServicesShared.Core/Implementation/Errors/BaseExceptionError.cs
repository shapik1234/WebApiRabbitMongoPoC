using System;

namespace ServicesShared.Core
{
    public class BaseExceptionError<TException> : IError
        where TException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseExceptionError{T}" /> class with original exception.
        /// </summary>
        /// <param name="exception">The original exception.</param>
        protected BaseExceptionError(TException exception)
        {
            Exception = exception;
            Description = exception.Message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseExceptionError{T}" /> class with error description.
        /// </summary>
        /// <param name="description">The error description.</param>
        protected BaseExceptionError(string description)
        {
            Exception = null;
            Description = description;
        }

        /// <summary>
        /// Gets an original exception.
        /// </summary>
        public TException Exception { get; }

        /// <inheritdoc />
        public override string ToString() => Description;

        #region IError implementation

        /// <inheritdoc cref="IError" />
        public virtual int? Code { get; } = null;

        /// <inheritdoc cref="IError" />
        public virtual string StatusCode => Code?.ToString();

        /// <inheritdoc cref="IError" />
        public virtual string Title { get; } = null;

        /// <inheritdoc cref="IError" />
        public virtual string Description { get; }

        #endregion
    }
}
