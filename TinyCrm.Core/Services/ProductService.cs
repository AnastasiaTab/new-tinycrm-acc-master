using System.Linq;
using System.Collections.Generic;

using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using System;

namespace TinyCrm.Core.Services
{
      /// <summary>
    /// 
    /// </summary>
    public class ProductService : IProductService
    {
        private TinyCrm.Core.Data.TinyCrmDbContext context;
        public ProductService(Data.TinyCrmDbContext dbContext)
        {
           context=dbContext;
        }
        private List<Product> ProductsList = new List<Product>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public ApiResult<Product> AddProduct(AddProductOptions options)
        {
            var result = new ApiResult<Product>();

            if (options == null) {
                result.ErrorCode = StatusCode.BadRequest;
                result.ErrorText = "null options";
                return result;
            }

            if (string.IsNullOrWhiteSpace(options.Name)) {
                result.ErrorCode = StatusCode.BadRequest;
                result.ErrorText = "null or empty name";
                return result;
            }

            if (options.Price <= 0) {
                result.ErrorCode = StatusCode.BadRequest;
                result.ErrorText = "negative or zero price";
                return result;
               
            }

            if (options.ProductCategory ==
              ProductCategory.Invalid) {
                result.ErrorCode = StatusCode.BadRequest;
                result.ErrorText = "invalid category ";
                return result;
            }

            var product = new Product() {
                Name = options.Name,
                Price = options.Price,
                Category = options.ProductCategory
            };

            context.Add(product);
            context.SaveChanges();

            result.ErrorCode = StatusCode.Success;
            result.Data = product;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public bool UpdateProduct(Guid productId,
            UpdateProductOptions options)
        {
            if (options == null) {
                return false;
            }

            var product = GetProductById(productId);
            if (product == null) { 
                return false; 
            }

            if (!string.IsNullOrWhiteSpace(options.Description)) {
                product.Description = options.Description;
            }

            if (options.Price != null &&
              options.Price <= 0) {
                return false;
            }

            if (options.Price != null) {
                if (options.Price <= 0) {
                    return false;
                } else {
                    product.Price = options.Price.Value;
                }
            }

            if (options.Discount != null &&
              options.Discount < 0) {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProductById(Guid id)
        {
            return context
                .Set<Product>()
                .SingleOrDefault(s => s.Id == id);
        }
        public int SumOfStocks()
        {
            var sum = context.Set<Product>().AsQueryable().
                Sum(c => c.InStock);

            return sum;
        }
        public List<Product> SearchProduct
            (SearchProductOptions searchProductOptions)
        {
            if (searchProductOptions == null)
            {
                return null;
            }

            var query = context.Set<Product>().AsQueryable();

            
            if (searchProductOptions.Description != null)
            {
                query = query.
                Where(c => c.Description == searchProductOptions.Description);
            }
            if (searchProductOptions != null &&
                searchProductOptions.MaxPrice > 0)
            {
                query = query.
                Where(c => c.Price <= searchProductOptions.MaxPrice);
            }

            if (searchProductOptions != null &&
                searchProductOptions.MinPrice > 0)
            {
                query = query.
                Where(c => c.Price >= searchProductOptions.MinPrice);
            }
            List<Product> products = query.ToList();
            return products;


        }
    }
}
