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
            context = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public ApiResult<Product> AddProduct(AddProductOptions options)
        {
            //var result = new ApiResult<Product>();

            if (options == null)
            {
                return new ApiResult<Product>(
                    StatusCode.BadRequest, "null options");
            }

            if (string.IsNullOrWhiteSpace(options.Name))
            {
                return new ApiResult<Product>(
                     StatusCode.BadRequest, "null or empty name");
            }

            if (options.Price <= 0)
            {
                return new ApiResult<Product>(
                      StatusCode.BadRequest, "negative or zero price");

            }

            if (options.ProductCategory ==
              ProductCategory.Invalid)
            {
                return new ApiResult<Product>(
                    StatusCode.BadRequest, "invalid category");
            }

            var product = new Product()
            {
                Name = options.Name,
                Price = options.Price,
                Category = options.ProductCategory
            };

            context.Add(product);
            context.SaveChanges();

            return ApiResult<Product>.CreateSuccessful(product);
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
            if (options == null)
            {
                return false;
            }

            var presult = GetProductById(productId);
            if (!presult.Success)
            {
                return false;
            }
            var product = presult.Data;

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                product.Description = options.Description;
            }

            if (options.Price != null &&
              options.Price <= 0)
            {
                return false;
            }

            if (options.Price != null)
            {
                if (options.Price <= 0)
                {
                    return false;
                }
                else
                {
                    product.Price = options.Price.Value;
                }
            }

            if (options.Discount != null &&
              options.Discount < 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApiResult<Product> GetProductById(Guid id)
        {
            var product = context
                .Set<Product>()
                .SingleOrDefault(s => s.Id == id);

            if (product == null)
            {
                return new ApiResult<Product>(
                    StatusCode.NotFound, $"Product {id} not found");
            }

            return ApiResult<Product>.CreateSuccessful(product);
        }

        public int SumOfStocks()
        {
            var sum = context.Set<Product>().AsQueryable().
                Sum(c => c.InStock);

            return sum;
        }
    }
}
