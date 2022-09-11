using System;
using System.Collections.Generic;
using System.Text;

namespace KolmeoAPI.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedOn { get; set; }
    }
}
