using CustomerApi.Data.Database;
using CustomerApi.Data.Entities;

namespace CustomerApi.Data.Repository.v1
{
    public class CustomerRepository : MongoRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext customerContext) 
            : base(customerContext)
        {
        }      
    }
}