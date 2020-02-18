using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private TinyCrmDbContext context;

        public CustomerService(TinyCrmDbContext dbContext)
        {
            context = dbContext;
        }

        public ApiResult<Customer> GetCustomerById(
            int customerId)
        {
            var customer = Search(
                new SearchCustomerOptions()
                {
                    Id = customerId
                }).SingleOrDefault();

            if (customer == null)
            {
                return new ApiResult<Customer>(
                    StatusCode.NotFound, $"Customer {customerId} not found");
            }

            return ApiResult<Customer>.CreateSuccessful(customer);
            
        }

        public IQueryable<Customer> Search(
            SearchCustomerOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var query = context
                .Set<Customer>()
                .AsQueryable();

            if (options.Id != null)
            {
                query = query.Where(
                    c => c.Id == options.Id);
            }

            if (options.VatNumber != null)
            {
                query = query.Where(
                    c => c.VatNumber == options.VatNumber);
            }

            if (options.Email != null)
            {
                query = query.Where(
                    c => c.Email == options.Email);
            }

            if (!string.IsNullOrWhiteSpace(options.FistName))
            {
                query = query
                      .Where(c => c.FirstName.Contains(options.FistName));
            }

            return query;
        }

        public ApiResult<Customer> Create(
            CreateCustomerOptions options)
        {
            if (options == null)
            {
                return new ApiResult<Customer>(
                    StatusCode.BadRequest, "null options");
            }

            if (string.IsNullOrWhiteSpace(options.Email) ||
                !options.Email.Contains("@") ||
                string.IsNullOrWhiteSpace(options.VatNumber))
            {
                return new ApiResult<Customer>(
                    StatusCode.BadRequest, "null or wrong options");
            }

            var customer = new Customer();
            customer.VatNumber = options.VatNumber;
            customer.Email = options.Email;
            customer.FirstName = options.FirstName;

            context.Set<Customer>().Add(customer);
            context.SaveChanges();

            return ApiResult<Customer>.CreateSuccessful(customer) ;
        }
    }
}
