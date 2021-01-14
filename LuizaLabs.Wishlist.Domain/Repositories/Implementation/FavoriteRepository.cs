using LuizaLabs.Wishlist.Domain.Database;
using LuizaLabs.Wishlist.Domain.Entities;
using LuizaLabs.Wishlist.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuizaLabs.Wishlist.Domain.Repositories.Implementation
{
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        private PostgresContext _db;
        
        public FavoriteRepository(PostgresContext db)
        {
            _db = db;
        }

        public Task<List<Favorite>> GetAllClientFavorites(Guid clientId)
        {
            
            return _db.Favorites.Where(f => f.ClientId == clientId).ToListAsync();
        }

        public Task<Favorite> GetFavorite(Guid clientId, Guid productId)
        {
            return _db.Favorites.Where(f => f.ClientId == clientId && f.ProductId == productId).FirstOrDefaultAsync();
        }
    }
}
