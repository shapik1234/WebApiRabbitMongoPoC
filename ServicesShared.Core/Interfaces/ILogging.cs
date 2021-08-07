using System;

namespace ServicesShared.Core
{
	public interface ILogging
	{
        /// <summary>
        /// Handles an error as message.
        /// </summary>
        /// <param name="message">The message.</param>
        void HandleError(string message);

        /// <summary>
        /// Handles an error as <see cref="IError" />.
        /// </summary>
        /// <param name="error">The error.</param>
        void HandleError(IError error);

        /// <summary>
        /// Handles an error as <see cref="Exception" />.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void HandleError(Exception exception);

        /// <summary>
        /// Handles debug information.
        /// </summary>
        /// <param name="message">The message.</param>
        void HandleDebug(string message);

        /// <summary>
        /// Handles custom information.
        /// </summary>
        /// <param name="message">The message.</param>
        void HandleInfo(string message);
    }
}
