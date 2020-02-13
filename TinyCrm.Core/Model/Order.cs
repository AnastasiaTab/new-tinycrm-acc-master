using System;
using System.Collections.Generic;

namespace TinyCrm.Core.Model
{
    public class Order
    {
        public Guid Id { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; } 
        public Status Status { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }

        public Order()
        {
            OrderProducts = new List<OrderProduct>();
        }
    }
}
