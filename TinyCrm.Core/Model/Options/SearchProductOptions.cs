using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCrm.Core.Model.Options
{
    public class SearchProductOptions
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public int Stock { get; set; }
    }
}
