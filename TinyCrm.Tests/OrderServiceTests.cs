using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;

using Xunit;

namespace TinyCrm.Tests
{
    public class OrderServiceTests :
        IClassFixture<TinyCrmFixture>
    {
        private ICustomerService customer_;
        private IProductService products_;
        private TinyCrmDbContext context_;

        public OrderServiceTests(
            TinyCrmFixture fixture)
        {
            context_ = fixture.Context;
            customer_ = fixture.Customers;
            products_ = fixture.Products;
        }

        [Fact]
        public void CreateOrder()
        {
            // Step 1: Create products
            var poptions = new AddProductOptions()
            {
                Name = "product 1",
                Price = 155.33M,
                ProductCategory = ProductCategory.Computers
            };

            var presult1 = products_.AddProduct(poptions);
            Assert.Equal(Core.StatusCode.Success, presult1.ErrorCode);

            poptions = new AddProductOptions()
            {
                Name = "product 2",
                Price = 113.33M,
                ProductCategory = ProductCategory.Computers
            };

            var presult2 = products_.AddProduct(poptions);
            Assert.Equal(Core.StatusCode.Success, presult2.ErrorCode);

            // Step 2: Create a new customer
            var options = new CreateCustomerOptions()
            {
                FirstName = "Dimitris",
                VatNumber = $"11{DateTime.Now:fffffff}",
                Email = "dd@Codehub.com",
            };

            var customer = customer_.Create(options);
            Assert.NotNull(customer);

            // Step 3: Create the order
            var order = new Order() 
            {
                 DeliveryAddress = "Athens",
                 Status = Status.Pending,
                 CreatedDateTime = DateTimeOffset.Now
            };

            // Step 4: Add products
            order.OrderProducts.Add(
                new OrderProduct() {
                    Product = presult1.Data
                });
            order.OrderProducts.Add(
                new OrderProduct() {
                    Product = presult2.Data
                });

            customer.Orders.Add(order);
            context_.SaveChanges();

            var dbOrder = context_
                .Set<Order>()
                .SingleOrDefault(o => o.Id == order.Id);

            Assert.NotNull(dbOrder);
            Assert.Equal(order.DeliveryAddress, dbOrder.DeliveryAddress);
        }

        [Fact]
        public void GetOrder()
        {
            var orderId = Guid.Parse("4D719FAB-08C7-464D-F192-08D7B0912505");

            var products = context_
                .Set<Order>()
                .Where(o => o.Id == orderId)
                .SelectMany(o => o.OrderProducts)
                .Select(o => o.Product)
                .ToList();
        }
    }
}
