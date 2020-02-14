using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCrm.Core.Model.Options
{
    public class CreateOrderOptions
    {
        public int CustomerId { get; set; }
        public List<Product> Products { get; set; }
        public string Address { get; set; }
        

    }
}
