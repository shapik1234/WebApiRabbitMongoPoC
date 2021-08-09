using CustomerApi.Data.Entities;
using CustomerApi.Data.Options;
using Microsoft.Extensions.Options;

namespace CustomerApi.Data.Database
{
    public class CustomerContext : MongoContext<Customer>
    {    
        public CustomerContext(IOptions<MongoDatabaseConfiguration> settings) 
            : base(settings)
        {    
        }
	}
}
