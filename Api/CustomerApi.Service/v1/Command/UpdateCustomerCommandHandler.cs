﻿using System.Threading;
using System.Threading.Tasks;
using CustomerApi.Data.Entities;
using CustomerApi.Data.Repository.v1;
using CustomerApi.Messaging.Send.Sender.v1;
using MediatR;

namespace CustomerApi.Service.v1.Command
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerUpdateSender _customerUpdateSender;

        public UpdateCustomerCommandHandler(ICustomerUpdateSender customerUpdateSender, ICustomerRepository customerRepository)
        {
            _customerUpdateSender = customerUpdateSender;
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            _customerRepository.Update(request.Customer, cancellationToken);

            _customerUpdateSender.SendCustomer(request.Customer);

            return request.Customer;
        }
    }
}