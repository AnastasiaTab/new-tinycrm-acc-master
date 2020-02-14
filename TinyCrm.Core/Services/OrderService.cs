using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public class OrderService : IOrderService
    {
        private TinyCrm.Core.Data.TinyCrmDbContext context;
         
        public OrderService(Data.TinyCrmDbContext dbContext)
        {
            context = dbContext;
        }
        public Order CreateOrder(CreateOrderOptions options, 
            SearchCustomerOptions customerOptions,
            SearchProductOptions productOptions)
        {
            if(options == null)
            {
                return null;
            }
            if (options.CustomerId <=0)
            {
                return null;
            }
            var customers = new CustomerService(context);
            var newCustomers = customers.
                Search(customerOptions);
            var customer = newCustomers.FirstOrDefault();
            
            if (customer == null)
            {
                return null;
            }

            var products = new ProductService(context);
            var newProducts = products.
                SearchProduct(productOptions);
            var product = newProducts.FirstOrDefault();

            var order = new Order()
            {
              DeliveryAddress=options.Address,
              CustomerId=options.CustomerId,
              Customer=customer
            };



            var op = new OrderProduct()
            {
                Order = order,
                OrderId = order.Id,
                Product=product,
                ProductId=product.Id
            };

            context
                .Set<Order>()
                .Add(order);
            context.SaveChanges();
            context.
                Set<OrderProduct>()
                .Add(op);
            context.SaveChanges();

            return order;


        }
        public List<Order> SearchOrder
            (SearchOrderOptions options)
        {
            if(options == null)
            {
                return null;
            }
            var query = context.Set<Order>()
                .AsQueryable();
            if(options.CustomerId != null)
            {
                query = query.Where(
                    c=>c.CustomerId==options.CustomerId);
            }
            if (options.OrderId != null)
            {
                query = query.Where(
                    c=>c.Id==options.OrderId);
            }
            return query.ToList();
        }
    }
}
