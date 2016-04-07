using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvcBasic.Models;

namespace MvcBasic.Repositories
{
    public class ProductRepository : IProductsRepository
    {
        private static readonly ConcurrentDictionary<int, Product> ProductsCache = 
            new ConcurrentDictionary<int, Product>();

        static ProductRepository()
        {
            ProductsCache.TryAdd(1, new Product { Id = 1, ProductName = "Ristretto", Price = 10});
            ProductsCache.TryAdd(2, new Product { Id = 2, ProductName = "Espresso", Price = 20 });
            ProductsCache.TryAdd(3, new Product { Id = 3, ProductName = "Macchiato", Price = 30 });
        }

        // Simulate async database queries and updates

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await Task.FromResult(ProductsCache.Values);
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await Task.FromResult(ProductsCache[id]);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await Task.FromResult(ProductsCache.TryAdd(product.Id, product));
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            await Task.FromResult(ProductsCache.TryUpdate(
                product.Id, product, ProductsCache[product.Id]));
            return product;
        }

        public async Task DeleteAsync(int id)
        {
            Product product;
            await Task.FromResult(ProductsCache.TryRemove(id, out product));
        }
    }
}
