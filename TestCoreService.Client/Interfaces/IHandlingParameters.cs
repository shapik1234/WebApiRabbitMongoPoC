using System;
using ServicesShared.Core;

namespace TestCoreService.Client
{
    internal interface IHandlingParameters : IBaseHandlingParameters
    {      

        /// <summary>
        /// Gets Sandbox value of UtcNow.
        /// </summary>
        DateTime? SandboxUtcNow { get; }

        /// <summary>
        /// Gets Db User Name.
        /// </summary>
        string DbUserName { get; }
    }
}
