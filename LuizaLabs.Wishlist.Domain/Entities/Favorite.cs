using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LuizaLabs.Wishlist.Domain.Entities
{
    public class Favorite
    {
        [JsonIgnore]
        public Guid ClientId { get; set; }
        [JsonIgnore]
        public Guid ProductId { get; set; }
        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
        public virtual Product Product { get; set; }
    }
}
