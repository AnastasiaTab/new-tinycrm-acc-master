using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCrm.Core.Model.Options
{
    public class SearchOrderOptions
    {
        public int? CustomerId { get; set; }
        public Guid? OrderId { get; set; }
        public string VatNumber { get; set; }
        public Status Status { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
    }
}
