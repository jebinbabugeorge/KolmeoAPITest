using KolmeoAPI.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KolmeoAPI.DALayer
{
    public interface IDataAccessLayer
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> FindProductAsync(int Id);

        Task<int> CreateProductAsync(Product product);
    }
}