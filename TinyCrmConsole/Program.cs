using System;
using System.Linq;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;

using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;

namespace TinyCrmConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new TinyCrmDbContext()) {
                IProductService productServices = 
                    new ProductService(context);
                //context.Add(
                //    new Product
                //    { 
                //        Name="pc",
                //        InStock=3,
                //        Id ="123"
                //    });
                //context.Add(
                //    new Product
                //    {
                //        Name = "mouse",
                //        InStock = 5,
                //        Id = "124"
                //    });
                //context.SaveChanges();
                var sum = productServices.SumOfStocks();
            }
        }
    }
}
