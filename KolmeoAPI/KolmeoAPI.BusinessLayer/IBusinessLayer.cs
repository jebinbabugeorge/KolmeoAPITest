using KolmeoAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KolmeoAPI.BLayer
{
    public interface IBusinessLayer
    {
        Task<IEnumerable<ProductDTO>> GetProductsAsync();
        Task<ProductDTO> GetProductAsync(int Id);

        Task<int> CreateProductAsync(ProductDTO productDto);
    }
}