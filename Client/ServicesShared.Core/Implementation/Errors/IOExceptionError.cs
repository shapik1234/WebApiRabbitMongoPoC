using System.IO;

namespace ServicesShared.Core
{
    public class IOExceptionError : BaseExceptionError<IOException>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IOExceptionError" /> class with original exception.
        /// </summary>
        /// <param name="exception">The original exception.</param>
        public IOExceptionError(IOException exception)
            : base(exception)
        {
        }

        #region Overridden members

        /// <inheritdoc cref="IError" />
        public override int? Code => Exception.HResult;

        #endregion
    }
}
