using System;

namespace CustomerApi.Messaging.Send.Listener.v1
{
    public interface ICustomerListener : IDisposable
    {
        void ListenCustomer(Action<string> action);
    }
}