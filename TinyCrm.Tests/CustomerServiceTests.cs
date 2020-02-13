using System;
using Xunit;

using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Tests
{
    public class CustomerServiceTests
    {
        private TinyCrmDbContext context_;

        public CustomerServiceTests()
        {
            context_ = new TinyCrmDbContext();
        }

        [Fact]
        public void CreateCustomer_Success()
        {
            ICustomerService customerService =
                new CustomerService(context_);

            var options = new CreateCustomerOptions()
            {
                Email = "dd@dd.gr",
                FirstName = "Dimmitris",
                VatNumber = "117008899",
            };
            var customer = customerService.Create(options);

            Assert.NotNull(customer);
            Assert.Equal(options.Email, customer.Email);
            Assert.Equal(options.VatNumber, customer.VatNumber);
            Assert.Equal(options.FirstName, customer.FirstName);

            var options1 = new SearchCustomerOptions()
            {
                Email = options.Email,
                FistName = options.FirstName,
                VatNumber = options.VatNumber
            };

            var customers = customerService.Search(options1);
            Assert.NotNull(customers);
            Assert.Single(customers);

            var dbCustomer = customers[0];
            Assert.Equal(customer.Id, dbCustomer.Id);
        }

        [Fact]
        public void CreateCustomer_Fail_Null_VatNumber()
        {
            ICustomerService customerService =
                new CustomerService(context_);

            var options = new CreateCustomerOptions()
            {
                Email = "dd@dd.gr",
                FirstName = "Dimmitris",
                VatNumber = null
            };
            var customer = customerService.Create(options);
            Assert.Null(customer);
        }

        [Fact]
        public void CreateCustomer_Fail_Email()
        {
            ICustomerService customerService =
                new CustomerService(context_);

            var options = new CreateCustomerOptions()
            {
                Email = "dddd.gr",
                FirstName = "Dimmitris",
                VatNumber = "12312312"
            };

            var customer = customerService.Create(options);
            Assert.Null(customer);

            options = new CreateCustomerOptions()
            {
                Email = "     ",
                FirstName = "Dimmitris",
                VatNumber = "12312312"
            };

            customer = customerService.Create(options);
            Assert.Null(customer);

            options = new CreateCustomerOptions()
            {
                Email = null,
                FirstName = "Dimmitris",
                VatNumber = "12312312"
            };

            customer = customerService.Create(options);
            Assert.Null(customer);
        }
    }
}
