using System;

namespace TinyCrm.Core.Model
{
    public class Product
    { 
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ProductCategory Category { get; set; }

        public int InStock { get; set; }

        public Product()
        {
            int counter=0;
            counter++;
        }
    }
}
