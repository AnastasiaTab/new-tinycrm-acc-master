using TinyCrm.Core.Services;
using TinyCrm.Core.Data;
using System;

namespace TinyCrm.Tests
{
    public class TinyCrmFixture : IDisposable
    {
        public TinyCrmDbContext Context { get; private set; }
        public IProductService Products { get; private set; }
        public ICustomerService Customers { get; private set; }

        public IOrderService Orders { get; private set; }
        public IReportSevice Reports { get; private set; }

        public TinyCrmFixture()
        {
            Context = new TinyCrmDbContext();
            Products = new ProductService(Context);
            Customers = new CustomerService(Context);
            Orders = new OrderService(
                Context
                , Customers
                , Products);
            Reports = new ReportService(
                Context, Customers,
                Products, Orders);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
