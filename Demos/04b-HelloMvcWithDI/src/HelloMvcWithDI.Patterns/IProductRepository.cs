using System.Collections.Generic;
using System.Threading.Tasks;
using HelloMvcWithDI.Entities;

namespace HelloMvcWithDI.Patterns
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetProduct(int id);
    }
}
