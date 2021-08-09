using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using ServicesShared.Core;
using TestCoreService.Client.Implementation.Handlers;

namespace TestCoreService.Client
{
    public class ServiceInstance : IWindowsService
    {
        private CancellationTokenSource cancellationToken;
        private readonly IHandlingParameters handlingParameters;

        public ServiceInstance(IHandlingParameters handlingParameters, ILoggerHandler log)
        {            
            this.handlingParameters = handlingParameters;
            this.Log = log;
        }

        #region Properties

        private readonly ILoggerHandler Log;

        private string ProductName { get; set; }

        private string Version { get; set; }

        #endregion

        private void SetupCancellationToken()
        {
            cancellationToken = new CancellationTokenSource();
        }

        #region IWindowsService implementation

        public void OnStart()
        {
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            ProductName = fileVersionInfo.OriginalFilename;
            Version = fileVersionInfo.FileVersion;

            Log.Debug($"{ProductName} v{Version} started");

            SetupCancellationToken();

            try
            {           
                Task.Factory
                    .StartNew(
                        new QueryMessagesHandler(cancellationToken, Log, handlingParameters).Start,
                        TaskCreationOptions.LongRunning);
            }
            catch (OperationCanceledException)
            {
                Log.Debug("Termination request has been received. Service will be stopped now.");
                cancellationToken.Cancel();
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);

                cancellationToken.Cancel();
            }
        }

        public void OnStop()
        {
            Log.Debug($"{ProductName} v{Version} stopped");

            cancellationToken.Cancel();
            cancellationToken.Dispose();
        }

        #endregion
    }
}