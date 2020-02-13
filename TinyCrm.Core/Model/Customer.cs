using System.Collections.Generic;

namespace TinyCrm.Core.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string VatNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }
        public int Age { get; set; }

        public List<Order> Orders { get; set; }

        public Customer()
        {
            Orders = new List<Order>();
        }
    }
}
