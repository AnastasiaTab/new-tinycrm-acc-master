using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public interface IReportSevice
    {
        public List<Product> Top10SoldProducts();

        public List<Customer> Top10CustomersByGross();

        public ApiResult<int> TotalSales(SearchOrderOptions options);

        public ApiResult<int> TotalOrderState(SearchOrderOptions options);
    }
}
