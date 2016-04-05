using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloMvcWithDI.Entities;
using Microsoft.Data.Entity;

namespace HelloMvcWithDI.Patterns.EF
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductsDb _dbContext;

        public ProductRepository(ProductsDb dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _dbContext.Products
                .OrderBy(e => e.ProductName)
                .ToListAsync();
            return products;
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _dbContext.Products
                .SingleOrDefaultAsync(e => e.Id == id);
            return product;
        }
    }
}
