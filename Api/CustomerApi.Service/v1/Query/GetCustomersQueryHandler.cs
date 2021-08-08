using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CustomerApi.Data.Entities;
using CustomerApi.Data.Repository.v1;
using MediatR;

namespace CustomerApi.Service.v1.Query
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<Customer>>
    {
        private readonly ICustomerRepository customerRepository;

        public GetCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<List<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return await customerRepository.Get(cancellationToken);
        }
    }
}