using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using System.Linq;

namespace TinyCrm.Core.Services
{
    public interface ICustomerService
    {
        public IQueryable<Customer> Search(
            SearchCustomerOptions options);

        public ApiResult<Customer> Create(CreateCustomerOptions options);
        public ApiResult<Customer> GetCustomerById(int customerId);
    }
}
