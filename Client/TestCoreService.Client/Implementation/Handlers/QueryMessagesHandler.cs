using CustomerApi.Messaging.Send.Listener.v1;
using ServicesShared.Core;
using System;
using System.Threading;

namespace TestCoreService.Client.Implementation.Handlers
{
	internal class QueryMessagesHandler : BaseHandler
	{
		private readonly ICustomerListener customerListener;
		public QueryMessagesHandler(
		   CancellationTokenSource cancellationToken,
		   ILoggerHandler log,
		   ICustomerListener customerMessagingListener)
			: base(cancellationToken, log)
		{
			this.customerListener = customerMessagingListener;
		}

		public override void Start()
		{
			try
			{
				IError error = null;

				do
				{
					//add action to do
					customerListener.ListenCustomer(m => Log.Information(m));

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
