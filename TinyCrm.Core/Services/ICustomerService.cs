using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public interface ICustomerService
    {
        public List<Customer> Search(
            SearchCustomerOptions options);

        public Customer Create(CreateCustomerOptions options);
    }
}
