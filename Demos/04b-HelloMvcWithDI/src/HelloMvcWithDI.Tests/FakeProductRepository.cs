using System.Collections.Generic;
using System.Threading.Tasks;
using HelloMvcWithDI.Entities;
using HelloMvcWithDI.Patterns;

namespace HelloMvcWithDI.Tests
{
    public class FakeProductRepository : IProductRepository
    {
        private List<Product> _products = new List<Product>
        {
            new Product
            {
                Id = 1,
                ProductName = "Espresso",
                Price = 10
            },
            new Product
            {
                Id = 2,
                ProductName = "Capuccino",
                Price = 20
            },
            new Product
            {
                Id = 3,
                ProductName = "Latte",
                Price = 30
            },
        };

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await Task.FromResult(_products);
            return products;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await Task.FromResult(_products[id - 1]);
        }
    }
}
