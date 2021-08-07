using ServicesShared.Core;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestCoreService.Client.Implementation.Handlers
{
	internal class QueryMessagesHandler : BaseHandler
	{
		public QueryMessagesHandler(
		   CancellationTokenSource cancellationToken,
		   ILoggingManager loggingManager,
		   ITimeProvider timeProvider,
		   IConnectionSettings connectionSettings,
		   IHandlingParameters handlingParameters)
			: base(cancellationToken, loggingManager.Get(typeof(QueryMessagesHandler)), timeProvider, connectionSettings, handlingParameters)
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
				Log.HandleDebug("Termination request has been received. Service will be stopped now.");
				CancellationToken.Cancel();
			}
			//catch (SqlException exception)
			//{
			//	Log.HandleError(HandleException(exception));
			//}
			catch (Exception exception)
			{
				Log.HandleError(HandleException(exception));
				CancellationToken.Cancel();
			}
		}

		#region Error handling methods

		private IError HandleException(Exception exception)
			=> new UnhandledError(exception.RenderDetails());

		private IError HandleException(AggregateException exception)
		{
			IError error = null;
			foreach (var innerException in exception.InnerExceptions)
			{
				var httpRequestException = innerException as HttpRequestException;
				var webException = httpRequestException?.InnerException as WebException;

				if (webException != null)
				{
					error = HandleException(webException);
				}
				else
				{
					var axiaException = innerException;
					if (axiaException != null)
					{
						error = HandleException(axiaException);
					}
				}
			}

			return error ?? new UnhandledError(exception.RenderDetails());
		}



		#endregion
	}
}
