using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TinyCrm.Tests
{
    public class ReportServiceTests : IClassFixture<TinyCrmFixture>
    {
        private ICustomerService customer_;
        private IProductService products_;
        private TinyCrmDbContext context_;
        private IOrderService order_;
        private IReportSevice report_;
        private ProductServiceTests productServiceTests_;
        private CustomerServiceTests customerServiceTests_;
        private OrderServiceTests orderServiceTests_;

        public ReportServiceTests(
            TinyCrmFixture fixture)
        {
            productServiceTests_ =
                  new ProductServiceTests(fixture);
            context_ = fixture.Context;
            customer_ = fixture.Customers;
            products_ = fixture.Products;
            order_ = fixture.Orders;
            report_ = fixture.Reports;
            customerServiceTests_ =
                new CustomerServiceTests(fixture);
            orderServiceTests_ = new OrderServiceTests(fixture);
        }

        [Fact]
        public void Get_Top10_SoldOut_Products()
        {
            var topProd = report_.Top10SoldProducts();
            Assert.NotEmpty(topProd);
            
        }


        [Fact]
        public void Get_Top10_CustomersByGross()
        {
            var topCust = report_.Top10CustomersByGross();
            Assert.NotEmpty(topCust);
        }

        [Fact]
        public void GetTotalSales()
        {

            var options = new SearchOrderOptions
            {
                CreatedDateTime = new DateTimeOffset()
            };

            var total = report_.TotalSales(options);
            Assert.Equal(0, total.Data);
        }

        [Fact]
        public void GetTotalOrderState() 
        {
            var options = new SearchOrderOptions
            {
                Status = Core.Model.Status.Delivered
            };

            var total = report_.TotalOrderState(options);
            Assert.Equal(0, total.Data);
        }


    }
}
