using LuizaLabs.Wishlist.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuizaLabs.Wishlist.Domain.Repositories.Interfaces
{
    public interface IFavoriteRepository : IGenericRepository<Favorite>
    {
        Task<List<Favorite>> GetAllClientFavorites(Guid clientId);
        Task<Favorite> GetFavorite(Guid clientId, Guid productId);
    }
}
