using Microsoft.EntityFrameworkCore;

namespace TinyCrm.Core.Data
{
    public class TinyCrmDbContext : DbContext
    {
        private readonly string connectionString_;

        public TinyCrmDbContext()
        {
            connectionString_ =
                "Server=localhost;Database=tinycrm_acc_final; Integrated Security=SSPI;Persist Security Info=False;";
        }


            public TinyCrmDbContext(string connString)
        {
            connectionString_ = connString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Model.Customer>()
                .ToTable("Customer", "core");

            modelBuilder
                .Entity<Model.Customer>()
                .HasIndex(c => c.VatNumber)
                .IsUnique();
            
            modelBuilder
                .Entity<Model.Order>()
                .ToTable("Order", "core"); 
                          
                modelBuilder
                .Entity<Model.Customer>()
                .Property(c => c.VatNumber)
                .HasMaxLength(9)
                .IsFixedLength();

            modelBuilder
                   .Entity<Model.Product>()
                   .ToTable("Product", "core");

            modelBuilder
                   .Entity<Model.OrderProduct>()
                   .ToTable("OrderProduct", "core");

            modelBuilder
                .Entity<Model.OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

        }
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString_);
        }
    }
}
