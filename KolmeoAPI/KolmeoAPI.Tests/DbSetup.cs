using KolmeoAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KolmeoAPI.Tests
{
    public static class DbSetup
    {
        public static ProductContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ProductContext>()
            .UseInMemoryDatabase(databaseName: "ProductTestDatabase")
            .Options;
            return new ProductContext(options);
        }
    }
}
