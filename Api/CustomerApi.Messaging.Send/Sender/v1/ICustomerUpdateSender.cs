﻿using CustomerApi.Data.Entities;
using System;

namespace CustomerApi.Messaging.Send.Sender.v1
{
    public interface ICustomerUpdateSender : IDisposable
    {
        void SendCustomer(Customer customer);
    }
}