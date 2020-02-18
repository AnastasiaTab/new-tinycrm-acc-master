using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCrm.Core.Model.Options
{
    public class CreateOrderOptions
    {
        public int CustomerId { get; set; }

        public List<Guid> ProductIds { get; set; }

    }
}

