using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public interface IOrderService
    {
        public Order CreateOrder(CreateOrderOptions options,
            SearchCustomerOptions customerOptions,
            SearchProductOptions productOptions);
        public List<Order> SearchOrder
            (SearchOrderOptions options);
    }
}
