using System.Net;

namespace ServicesShared.Core
{
    public class ConnectFailureError : WebExceptionError, IError
    {
        public ConnectFailureError(WebExceptionStatus status, string description)
            : base(status, description)
        {
        }
    }
}
