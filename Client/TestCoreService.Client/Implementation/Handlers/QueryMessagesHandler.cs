using Service.Messaging.Listener.Listener.v1;
using ServicesShared.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestCoreService.Client.Implementation.Handlers
{
	internal class QueryMessagesHandler : BaseHandler
	{
		private readonly IConsumer customerListener;
		public QueryMessagesHandler(
		   CancellationTokenSource cancellationToken,
		   ILoggerHandler log,
		   IConsumer customerMessagingListener)
			: base(cancellationToken, log)
		{
			this.customerListener = customerMessagingListener;
		}

		public override void Start()
		{
			try
			{
				IError error = null;
				Log.Information("Message Query Handler is working...");

				customerListener.Listen(m => Log.Information(m));
				do
				{
					var now = DateTime.Now;
					//Idle(now, now, 2, nameof(QueryMessagesHandler));
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
			finally
			{
				customerListener?.Dispose();
			}
		}

		#region Error handling methods

		private IError HandleException(Exception exception)
			=> new UnhandledError(exception.RenderDetails());

		#endregion
	}
}
