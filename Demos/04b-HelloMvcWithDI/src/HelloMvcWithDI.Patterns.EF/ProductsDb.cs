using HelloMvcWithDI.Entities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace HelloMvcWithDI.Patterns.EF
{
    public class ProductsDb : DbContext
    {
        public ProductsDb(DbContextOptions options) :
            base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductName)
                .IsRequired();
        }
    }
}
