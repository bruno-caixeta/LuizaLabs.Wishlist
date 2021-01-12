using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LuizaLabs.Wishlist.Domain.ViewModels
{
    public class ClientViewModel
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
