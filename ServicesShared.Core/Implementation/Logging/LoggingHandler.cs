using Microsoft.Extensions.Logging;
using ServicesShared.Core.Extensions;
using System;

namespace ServicesShared.Core
{
    public class LoggingHandler<T> : ILogging
    {
        private ILogger<T> logger;
        public LoggingHandler(ILogger<T> log)
        {
            logger = log;
        }
               

        #region ILogging implementation

        /// <inheritdoc />
        public void HandleError(string message)
        {
            logger.LogError(message);
        }

        /// <inheritdoc />
        public void HandleError(IError error)
        {
            logger.LogError($"{Environment.NewLine}{error}");
        }

        /// <inheritdoc />
        public void HandleError(Exception exception)
        {
            logger.LogError($"{Environment.NewLine}{exception.RenderDetails()}");
        }

        /// <inheritdoc />
        public void HandleDebug(string message)
        {
            logger.LogDebug(message);
        }

        /// <inheritdoc />
        public void HandleInfo(string message)
        {
            logger.LogInformation(message);
        }

        #endregion
    }
}
