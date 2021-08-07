using System;
using System.Collections.Generic;
using System.Text;

namespace ServicesShared.Core
{
    public interface IConnectionSettings
    {
        /// <summary>
        /// Gets a name of server where database is deployed.
        /// </summary>
        string ServerName { get; }

        /// <summary>
        /// Gets a name of the database.
        /// </summary>
        string DatabaseName { get; }

        /// <summary>
        /// Gets a user name to access to database.
        /// </summary>
        string DbUserName { get; }

        /// <summary>
        /// Gets a password for provided <see cref="DbUserName" />.
        /// </summary>
        string DbPassword { get; }

        /// <summary>
        /// Gets a connection string.
        /// </summary>
        string ConnectionString { get; }
    }
}
