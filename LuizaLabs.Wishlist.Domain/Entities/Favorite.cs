using LuizaLabs.Wishlist.Domain.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuizaLabs.Wishlist.Domain.Entities
{
    public class Favorite
    {
        [Key]
        [JsonIgnore]
        public Guid ClientId { get; set; }
        [JsonIgnore]
        public Guid ProductId { get; set; }
        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
        public virtual Product Product { get; set; }


        public Favorite() { }

        public Favorite(FavoriteViewModel favoriteViewModel)
        {
            ClientId = favoriteViewModel.ClientId;
            ProductId = favoriteViewModel.ProductId;
        }
    }
}
