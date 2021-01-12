using System;
using System.Collections.Generic;
using System.Text;

namespace LuizaLabs.Wishlist.Domain.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
    }
}
