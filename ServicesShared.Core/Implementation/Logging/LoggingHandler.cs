using Serilog;
using System;

namespace ServicesShared.Core
{
    public class LoggerHandler : ILoggerHandler
    {
        private ILogger logger;
        public LoggerHandler(ILogger logger)
        {
            this.logger = logger;
        }
               

        #region ILogging implementation

        /// <inheritdoc />
        public void Error(string message)
        {
            logger.Error(message);
        }

        /// <inheritdoc />
        public void Error(IError error)
        {
            logger.Error($"{Environment.NewLine}{error}");
        }

        /// <inheritdoc />
        public void Error(Exception exception)
        {
            logger.Error($"{Environment.NewLine}{exception.RenderDetails()}");
        }

        /// <inheritdoc />
        public void Debug(string message)
        {
            logger.Debug(message);
        }

        /// <inheritdoc />
        public void Information(string message)
        {
            logger.Information(message);
        }

        #endregion
    }
}
