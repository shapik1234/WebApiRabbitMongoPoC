using System.Data.SqlClient;

namespace ServicesShared.Core
{
    public class SqlExceptionError : BaseExceptionError<SqlException>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlExceptionError" /> class with original exception.
        /// </summary>
        /// <param name="exception">The original exception.</param>
        public SqlExceptionError(SqlException exception)
            : base(exception)
        {
        }

        #region Overridden members

        /// <inheritdoc cref="IError" />
        public override int? Code => Exception.Number;

        #endregion
    }
}
