using System;

namespace LuizaLabs.Wishlist.Domain.Entities
{
    public class Product
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public decimal ReviewScore { get; set; }
    }
}
