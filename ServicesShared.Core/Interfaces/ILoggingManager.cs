using System;

namespace ServicesShared.Core
{
    public interface ILoggingManager
    {
        /// <summary>
        /// Returns a new instance of <see cref="ILogging" /> for <see cref="type">provided type</see>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A new instance of <see cref="ILogging" />.</returns>
        ILogging Get(Type type);

        /// <summary>
        /// Returns a new instance of <see cref="ILogging" /> for <see cref="name">provided name</see>.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A new instance of <see cref="ILogging" />.</returns>
        ILogging Get(string name);

        /// <summary>
        /// Setups a logging parameters.
        /// </summary>
        /// <param name="pathToLog">The path to save log files.</param>
        /// <returns>The name of log file.</returns>
        string Setup(string pathToLog);
    }
}
