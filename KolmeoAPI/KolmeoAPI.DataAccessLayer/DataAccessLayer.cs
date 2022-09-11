using KolmeoAPI.Data;
using KolmeoAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KolmeoAPI.DALayer
{
    public class DataAccessLayer : IDataAccessLayer
    {
        private readonly ProductContext _context;

        public DataAccessLayer(ProductContext context)
        {
            _context = context;
        }

        public async Task<int> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product.Id;
        }

        public async Task<Product> FindProductAsync(int Id)
        {
            return await _context.Products.FindAsync(Id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
