using System;

namespace Service.Messaging.Listener.Listener.v1
{
    public interface IConsumer : IDisposable
    {
        void Listen(Action<string> action);
    }
}