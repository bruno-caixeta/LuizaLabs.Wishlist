using LuizaLabs.Wishlist.Domain.Entities;
using LuizaLabs.Wishlist.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuizaLabs.Wishlist.Domain.Repositories.Implementation
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
    }
}
