using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCrm.Core.Model
{
    public class OrderProduct
    {
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }


    }
}
