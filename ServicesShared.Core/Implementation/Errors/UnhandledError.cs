using System;

namespace ServicesShared.Core
{
    public class UnhandledError : BaseExceptionError<Exception>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnhandledError" /> class with error description.
        /// </summary>
        /// <param name="description">The error description.</param>
        public UnhandledError(string description)
            : base (description)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnhandledError" /> class with original exception.
        /// </summary>
        /// <param name="exception">The original exception.</param>
        public UnhandledError(Exception exception)
            : base(exception)
        {
        }
    }
}
