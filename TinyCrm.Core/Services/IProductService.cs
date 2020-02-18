using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Model;
using System;

namespace TinyCrm.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        ApiResult<Product> AddProduct(AddProductOptions options);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        bool UpdateProduct(Guid productId,
            UpdateProductOptions options);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ApiResult<Product> GetProductById(Guid id);
        public int SumOfStocks();
    }
}
