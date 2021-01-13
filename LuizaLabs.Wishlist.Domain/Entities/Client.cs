using LuizaLabs.Wishlist.Domain.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace LuizaLabs.Wishlist.Domain.Entities
{
    public class Client
    {
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }


        public Client() { }

        public Client(ClientViewModel clientViewModel)
        {
            ClientId = Guid.NewGuid();
            Name = clientViewModel.Name;
            Email = clientViewModel.Email;
        }

        public void UpdateClientInstance(ClientViewModel clientViewModel)
        {
            Name = clientViewModel.Name;
            Email = clientViewModel.Email;
        }
    }
}
