using CustomerApi.Data.Entities;

namespace CustomerApi.Messaging.Send.Sender.v1
{
    public interface ICustomerListener
    {
        void SendCustomer(Customer customer);
    }
}