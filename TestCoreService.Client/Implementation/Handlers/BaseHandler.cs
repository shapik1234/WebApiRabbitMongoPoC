using Serilog;
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
            ILoggerHandler log,
            IHandlingParameters handlingParameters)
        {
            Log = log;
            CancellationToken = cancellationToken;
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
        protected ILoggerHandler Log;


        /// <summary>
        /// Gets a parameters that allows to manage communication with DataService.
        /// </summary>
        protected IHandlingParameters HandlingParameters { get; }

        #endregion

        public abstract void Start();
       

        protected void Idle(DateTime started, DateTime now, int duration, string kindOfProcess)
        {
            Log.Debug($"{kindOfProcess}s processing ended at {now:HH:mm:ss}");

            DateTime nextIteration = now.AddMinutes(duration);
            TimeSpan timeDifference = nextIteration - now;

            if (timeDifference.TotalMilliseconds > 0)
            {
                Log.Debug(
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
