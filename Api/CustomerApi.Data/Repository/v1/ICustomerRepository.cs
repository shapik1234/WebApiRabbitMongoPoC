using CustomerApi.Data.Entities;

namespace CustomerApi.Data.Repository.v1
{
    public interface ICustomerRepository : IMongoRepository<Customer>
    {
    }
}