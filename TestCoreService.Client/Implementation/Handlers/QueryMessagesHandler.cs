using ServicesShared.Core;
using System;
using System.Threading;

namespace TestCoreService.Client.Implementation.Handlers
{
	internal class QueryMessagesHandler : BaseHandler
	{
		public QueryMessagesHandler(
		   CancellationTokenSource cancellationToken,
		   ILoggerHandler log,
		   IHandlingParameters handlingParameters)
			: base(cancellationToken, log, handlingParameters)
		{

		}

		public override void Start()
		{
			try
			{
				IError error = null;

				do
				{

				} while (!CancellationToken.IsCancellationRequested);
			}
			catch (OperationCanceledException)
			{
				Log.Debug("Termination request has been received. Service will be stopped now.");
				CancellationToken.Cancel();
			}			
			catch (Exception exception)
			{
				Log.Error(HandleException(exception).Description);
				CancellationToken.Cancel();
			}
		}

		#region Error handling methods

		private IError HandleException(Exception exception)
			=> new UnhandledError(exception.RenderDetails());

		#endregion
	}
}
