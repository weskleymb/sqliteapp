using SqliteApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqliteApp.Daos
{
    public interface IProductsRepository
    {

        Boolean InsertProductAsync(Product product);

        Task<bool> DeleteProductAsync(int id);

        Task<bool> UpdateProductAsync(Product product);

        IList<Product> GetProductsAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task<IEnumerable<Product>> QueryProductsAsync(Func<Product, bool> predicate);

    }
}
