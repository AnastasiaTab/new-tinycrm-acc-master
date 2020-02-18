using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{


    public class OrderService : IOrderService
    {
        private readonly TinyCrmDbContext context_;

        private readonly ICustomerService customer_;

        private readonly IProductService product_;

        public OrderService(
            TinyCrmDbContext context,
            ICustomerService customers,
            IProductService products)
        {
            context_ = context;
            customer_ = customers;
            product_ = products;

        }
        public ApiResult<Order> CreateOrder(
            CreateOrderOptions createoptions)
        {
            if (createoptions == null)
            {
                return new ApiResult<Order>(
                    StatusCode.BadRequest, "null options");
            }

            var cresult = customer_
                .GetCustomerById(createoptions.CustomerId);
            if (!cresult.Success)
            {
                return ApiResult<Order>.Create(cresult);
            }

            var order = new Order();

            foreach (var id in createoptions.ProductIds)
            {
                var prodResult = product_
                     .GetProductById(id);

                if (!prodResult.Success)
                {
                    return ApiResult<Order>.Create(
                        prodResult);
                }

                order.OrderProducts.Add(
                    new OrderProduct()
                    {
                        Product = prodResult.Data
                    });

                //order.Customer.LastGross.Equals(prodResult.Data.Price);
            }
            
            context_.Add(order);
            cresult.Data.Orders.Add(order);
            
            context_.SaveChanges();

            return ApiResult<Order>.CreateSuccessful(order);
        }

        public ApiResult<IQueryable<Order>> SearchOrder
            (SearchOrderOptions options)
        {
            if (options == null)
            {
                return ApiResult<IQueryable<Order>>.CreateUnSuccessful(StatusCode.BadRequest,"null options");
            }

            if (options.OrderId==null
                && options.CustomerId == null
                && options.VatNumber == null)
            {
                return ApiResult<IQueryable<Order>>.CreateUnSuccessful(StatusCode.BadRequest, "null options");
            }
            var query = context_
                .Set<Order>()
                .AsQueryable();

            if (options.CustomerId != null)
            {
                query = query.Where(
                    c => c.CustomerId == options.CustomerId);
            }
            if (options.OrderId != null)
            {
                query = query.Where(
                    c => c.Id == options.OrderId);
            }
            if (options.VatNumber != null)
            {
                query = query.Where(
                  c => c.Customer.VatNumber.Equals(options.VatNumber));
            }
            return  ApiResult<IQueryable<Order>>.CreateSuccessful(query);
        }

    }
}
