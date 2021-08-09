using System;

namespace Service.Messaging.Listener.Listener.v1
{
    public interface ICustomerListener : IDisposable
    {
        void ListenCustomer(Action<string> action);
    }
}