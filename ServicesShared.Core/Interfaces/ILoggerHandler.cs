using System;

namespace ServicesShared.Core
{
	public interface ILoggerHandler
	{
        /// <summary>
        /// Handles an error as message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Error(string message);

        /// <summary>
        /// Handles an error as <see cref="IError" />.
        /// </summary>
        /// <param name="error">The error.</param>
        void Error(IError error);

        /// <summary>
        /// Handles an error as <see cref="Exception" />.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void Error(Exception exception);

        /// <summary>
        /// Handles debug information.
        /// </summary>
        /// <param name="message">The message.</param>
        void Debug(string message);

        /// <summary>
        /// Handles custom information.
        /// </summary>
        /// <param name="message">The message.</param>
        void Information(string message);
    }
}
