using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public class ReportService : IReportSevice
    {
        private TinyCrmDbContext context_;
        private readonly ICustomerService customer_;

        private readonly IProductService product_;
        private readonly IOrderService order_;

        public ReportService(TinyCrmDbContext context,
            ICustomerService customers,
            IProductService products,
            IOrderService orders)
        {
            context_ = context;
            customer_ = customers;
            product_ = products;
            order_ = orders;
        }
        public List<Product> Top10SoldProducts()
        {
            var top10Prod = context_.Set<Product>().
                OrderByDescending(p =>p.Id).
                Take(10);

            return top10Prod.ToList();
        }

        public List<Customer> Top10CustomersByGross()
        {
            var top10Cust = context_.Set<Customer>().
                OrderByDescending(c => c.TotalGross).
                Take(10);
            return top10Cust.ToList();
        }

        public ApiResult<int> TotalSales(SearchOrderOptions options)
        {
            if (options == null)
            {
                return new ApiResult<int>(
                    StatusCode.BadRequest, "null options");
            }
            if (options.CreatedDateTime == null)
            {
                return new ApiResult<int>(
                    StatusCode.BadRequest, "does not exist this period of time");
            }
            var result = context_.Set<Order>()
                .Count(o => o.CreatedDateTime == options.CreatedDateTime);
            return ApiResult<int>.CreateSuccessful(result);

        }
        public ApiResult<int> TotalOrderState(SearchOrderOptions options)
        {
            if (options == null)
            {
                return  new ApiResult<int>(
                    StatusCode.BadRequest, "null options"); 
            }
            if (options.Status != 0)
            {
                var result = context_.Set<Order>().Count(o => o.Status == options.Status);
                return ApiResult<int>.CreateSuccessful(result);
            }
            else
                return new ApiResult<int>(
                    StatusCode.BadRequest, "invalid Status");
        }
    }
}
