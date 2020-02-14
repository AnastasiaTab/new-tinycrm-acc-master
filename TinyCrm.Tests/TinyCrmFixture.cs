using TinyCrm.Core.Services;
using TinyCrm.Core.Data;

namespace TinyCrm.Tests
{
    public class TinyCrmFixture
    {
        public TinyCrmDbContext Context { get; private set; }
        public IProductService Products { get; private set; }
        public ICustomerService Customers { get; private set; }
        public IOrderService Orders { get; private set; }

        public TinyCrmFixture()
        {
            Context = new TinyCrmDbContext();
            Products = new ProductService(Context);
            Customers = new CustomerService(Context);
            Orders = new OrderService(Context);
        }
    }
}
