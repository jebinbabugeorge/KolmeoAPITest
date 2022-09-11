using KolmeoAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KolmeoAPI.Tests.SeedData
{
    public static class ProductSeedData
    {
        public static void Seed(ProductContext context)
        {
            if(context.Database.IsInMemory())
                context.Database.EnsureDeleted();

            for (int i = 1; i <= 5; i++)
            {
                var product = new Data.Models.Product
                {
                    Id = i,
                    Name = $"Product {i}",
                    Description = $"Product Description {i}",
                    Price = (decimal)(new Random().Next(1, 100) + new Random().NextDouble())
                };

                context.Products.Add(product);
            }

            context.SaveChanges();
        }
    }
}
