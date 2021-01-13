using System.ComponentModel.DataAnnotations;

namespace LuizaLabs.Wishlist.Domain.ViewModels
{
    public class ClientViewModel
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
