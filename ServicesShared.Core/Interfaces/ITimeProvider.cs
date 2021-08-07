using System;

namespace ServicesShared.Core
{
    public interface ITimeProvider
    {
        DateTime Now { get; }

        DateTime Today { get; }
    }
}
