using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TinyCrm.Core;
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
        private IOrderService order_;
        private ProductServiceTests productServiceTests_;
        private CustomerServiceTests customerServiceTests_;
        //private OrderServiceTests orderServiceTests_;
        public OrderServiceTests(
            TinyCrmFixture fixture)
        {
            productServiceTests_ =
                  new ProductServiceTests(fixture);
            context_ = fixture.Context;
            customer_ = fixture.Customers;
            products_ = fixture.Products;
            order_ = fixture.Orders;
            customerServiceTests_ =
                new CustomerServiceTests(fixture);
            //orderServiceTests_ = new OrderServiceTests(fixture);
        }

        [Fact]
        public Order CreateOrder_Success()
        {
            var customer = customerServiceTests_
                .CreateCustomer_Success();
            var p1 = productServiceTests_.AddProduct_Success();
            var p2 = productServiceTests_.AddProduct_Success();

            var orderOptions = new CreateOrderOptions
            {
                CustomerId = customer.Id,
                ProductIds = new List<Guid>() { p1.Id, p2.Id }
            };
            var createorder = order_.CreateOrder(orderOptions);

            Assert.True(createorder.Success);


            var orderId = createorder.Data.Id;
            var order = context_.Set<Order>()
                //.Include(o=> o.OrderProducts)
                .Where(o => o.Id == orderId)
                .SingleOrDefault();
            Assert.NotNull(order);

            foreach (var id in orderOptions.ProductIds)
            {
                var op = order.OrderProducts
                    .Where(p => p.ProductId == id)
                    .SingleOrDefault();

                Assert.NotNull(op);
            }
            return order;
        }

        [Fact]
        public void GetOrder()
        {
            var c1 = new TinyCrmDbContext();
            var c2 = new TinyCrmDbContext();

            var customer = new Customer()
            {
                FirstName = "Dimitris",
                VatNumber = $"123{DateTimeOffset.Now:ffffff}"
            };

            c1.Add(customer);
            c1.SaveChanges();

            //customer.FirstName = "eleana";
            //var cust = customer;
            //cust.Id = 0;

            ////c1.Add(customer);
            ////c1.SaveChanges();

            //c2.Add(customer);
            //c2.SaveChanges();

            var orderId = Guid.Parse("3C516069-F3A1-4CB7-8225-08D7B14E7C62");

            var q = context_
                .Set<OrderProduct>()
                .Where(o => o.OrderId == orderId)
                .Select(o => o.Order.Customer);
            //.ToList();

            var ord = q.ToList();


            var order = context_
                .Set<Order>()
                .ToList()
                .Where(o => o.Id == orderId);

        }

        [Fact]
        public void SearchOrder_Success()
        {
            var createOrder = CreateOrder_Success();
            var options = new SearchOrderOptions()
            {
                CustomerId = createOrder.CustomerId,
                OrderId = createOrder.Id,
                VatNumber = createOrder.Customer.VatNumber
            };
            var results = order_.SearchOrder(options);
            Assert.Equal(StatusCode.Success, results.ErrorCode);
        }

    }
}

