using System;
using Xunit;

using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using TinyCrm.Core.Model.Options;
using System.Linq;

namespace TinyCrm.Tests
{
    public class CustomerServiceTests: IClassFixture<TinyCrmFixture>
    {
        private TinyCrmDbContext context_;
        
        public CustomerServiceTests(TinyCrmFixture fixture)
        {
            context_ = fixture.Context;
        }

        [Fact]
        public Customer CreateCustomer_Success()
        {
            ICustomerService customerService =
                new CustomerService(context_);

            var options = new CreateCustomerOptions()
            {
                Email = "dd@dd.gr",
                FirstName = "Dimmitris",
                VatNumber = $"111{DateTimeOffset.Now:ffffff}",
            };
            var customer = customerService.Create(options);

            Assert.NotNull(customer);
            //Assert.Equal(options.Email, customer.Email);
            //Assert.Equal(options.VatNumber, customer.VatNumber);
            //Assert.Equal(options.FirstName, customer.FirstName);

            var options1 = new SearchCustomerOptions()
            {
                Email = options.Email,
                FistName = options.FirstName,
                VatNumber = options.VatNumber
            };

            var dbCustomer = customerService
                .Search(options1)
                .Where(c => c.Id == customer.Data.Id)
                .SingleOrDefault();

            Assert.NotNull(customer);

            Assert.Equal(customer.Data.Id, dbCustomer.Id);
            return customer.Data;
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
            Assert.Null(customer.Data);
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
            Assert.Null(customer.Data);

            options = new CreateCustomerOptions()
            {
                Email = "     ",
                FirstName = "Dimmitris",
                VatNumber = "12312312"
            };

            customer = customerService.Create(options);
            Assert.Null(customer.Data);

            options = new CreateCustomerOptions()
            {
                Email = null,
                FirstName = "Dimmitris",
                VatNumber = "12312312"
            };

            customer = customerService.Create(options);
            Assert.Null(customer.Data);
        }
    }
}
