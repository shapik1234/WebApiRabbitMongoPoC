using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesShared.Core.Implementation.Logging
{
    public class LoggingManager : ILoggingManager
    {
        /// <inheritdoc />
        public ILogging Get<T>(T type) => new LoggingHandler(ILogging));

        /// <inheritdoc />
        public ILogging Get(string name) => new LoggingHandler(LogManager.GetLogger(name));

        /// <inheritdoc />
        public string Setup(string pathToLog)
        {
            XmlConfigurator.Configure();

            string filename = string.Empty;

            RollingFileAppender fileAppender =
                LogManager.GetRepository().GetAppenders().FirstOrDefault(appender => appender is RollingFileAppender) as RollingFileAppender;

            if (fileAppender != null && !fileAppender.File.IsNullOrEmpty())
            {
                filename = Path.GetFileName(fileAppender.File);

                if (!pathToLog.IsNullOrEmpty() && filename != null)
                {
                    try
                    {
                        fileAppender.File = Path.Combine(pathToLog, filename);
                        fileAppender.ActivateOptions();
                    }
                    catch (PathTooLongException)
                    {
                        // Keep default path without changes.
                    }
                }
            }

            return filename;
        }
    }
}
