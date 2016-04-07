﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MvcBasic.Models;

namespace MvcBasic.Repositories
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product> GetProductAsync(int id);

        Task<Product> CreateProductAsync(Product product);

        Task<Product> UpdateProductAsync(Product product);

        Task DeleteAsync(int id);
    }
}
