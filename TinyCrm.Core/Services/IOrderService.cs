using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public interface IOrderService
    {
        ApiResult<Order> CreateOrder(CreateOrderOptions options);
        public ApiResult<IQueryable<Order>> SearchOrder
            (SearchOrderOptions options);
    }
}
