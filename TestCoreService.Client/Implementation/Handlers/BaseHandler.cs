using ServicesShared.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TestCoreService.Client.Implementation.Handlers
{
    internal abstract class BaseHandler
    {
        protected BaseHandler(
            CancellationTokenSource cancellationToken,
            ILogging log,
            ITimeProvider timeProvider,
            IConnectionSettings connectionSettings,
            IHandlingParameters handlingParameters)
        {
            CancellationToken = cancellationToken;
            Log = log;
            TimeProvider = timeProvider;
            ConnectionSettings = connectionSettings;
            HandlingParameters = handlingParameters;
        }

        #region Properties

        /// <summary>
        /// Gets an instance of the cancellation token.
        /// </summary>
        protected CancellationTokenSource CancellationToken { get; }

        /// <summary>
        /// Gets a logging instance.
        /// </summary>
        protected ILogging Log { get; }

        /// <summary>
        /// Gets an interface that provides date/time.
        /// </summary>
        protected ITimeProvider TimeProvider { get; }

        /// <summary>
        /// Gets a connection settings to database.
        /// </summary>
        protected IConnectionSettings ConnectionSettings { get; }

        /// <summary>
        /// Gets a parameters that allows to manage communication with DataService.
        /// </summary>
        protected IHandlingParameters HandlingParameters { get; }

        #endregion

        public abstract void Start();

        protected void HandleFailedRequest(IError error)
        {
            string message =
                new StringBuilder()
                    .AppendLine($"Date: {TimeProvider.Now:MM/dd/yyyy hh:mm:ss}")
                    .AppendLine("Status: Error")
                    .AppendLine($"Description: {error.Title}")
                    .AppendLine($"Details: {error.Description}")
                    .ToString();

            Log.HandleError($"{Environment.NewLine}{message}");
        }

        protected void Idle(DateTime started, DateTime now, int duration, string kindOfProcess)
        {
            Log.HandleDebug($"{kindOfProcess}s processing ended at {now:HH:mm:ss}");

            DateTime nextIteration = now.AddMinutes(duration);
            TimeSpan timeDifference = nextIteration - now;

            if (timeDifference.TotalMilliseconds > 0)
            {
                Log.HandleDebug(
                    string.Format(
                        "Next iteration will start in {0:D2}:{1:D2}:{2:D2} {3:MM/dd/yyyy}",
                        timeDifference.Days * 24 + timeDifference.Hours,
                        timeDifference.Minutes,
                        timeDifference.Seconds,
                        nextIteration));

                Thread.Sleep((int)timeDifference.TotalMilliseconds);
            }
        }       
    }
}
