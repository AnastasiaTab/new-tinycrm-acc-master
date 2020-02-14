using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;
using TinyCrm.Core.Data;

using Xunit;

namespace TinyCrm.Tests
{
    public class ProductServiceTests :
        IClassFixture<TinyCrmFixture>
    {
        private TinyCrmDbContext  context_;
        private IProductService products_;

        public ProductServiceTests(
            TinyCrmFixture fixture)
        {
            context_ = fixture.Context;
            products_ = fixture.Products;
        }

        [Fact]
        public void AddProduct_Success()
        {
            var options = new AddProductOptions()
            {
                Name = "mobile",
                Price = 10m,
                ProductCategory = Core.Model.ProductCategory.Computers
            };

            var result = products_.AddProduct(options);
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal(Core.StatusCode.Success, result.ErrorCode);
        }
        [Fact]
        public void SearchProduct_Success()
        {
            
            var options = new SearchProductOptions()
            {
                MaxPrice = 500m,
                MinPrice = 20m
            };

            var products = products_.SearchProduct(options);
            Assert.NotEmpty(products);
        }
    }
}
