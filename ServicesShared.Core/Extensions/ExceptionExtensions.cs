using System;
using System.Text;

namespace ServicesShared.Core
{
    public static class ExceptionExtensions
    {
        public static string RenderDetails(this Exception exception, bool renderStackTrace = true)
        {
            var exceptionDetails = new StringBuilder();

            var aggregateException = exception as AggregateException;
            if (aggregateException != null)
            {
                foreach (Exception innerException in aggregateException.InnerExceptions)
                {
                    exceptionDetails.AppendLine(innerException.RenderDetails(renderStackTrace));
                }
            }
            else if (exception != null)
            {
                exceptionDetails.AppendFormat("Error type: {0}", exception.GetType()).AppendLine();
                exceptionDetails.AppendFormat("Error message: {0}", exception.Message).AppendLine();

                if (!exception.StackTrace.IsNullOrEmpty() && renderStackTrace)
                {
                    exceptionDetails.Append("Stack trace:").AppendLine().Append(exception.StackTrace);
                }

                if (exception.InnerException != null)
                {
                    exceptionDetails.AppendLine().Append("Inner Exception:").AppendLine();
                    exceptionDetails.AppendLine(exception.InnerException.RenderDetails(renderStackTrace));
                }
            }

            return exceptionDetails.ToString();
        }
    }
}
