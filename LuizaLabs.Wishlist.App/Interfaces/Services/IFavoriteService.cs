using LuizaLabs.Wishlist.App.ResponseModels;
using LuizaLabs.Wishlist.Domain.Entities;
using LuizaLabs.Wishlist.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuizaLabs.Wishlist.App.Interfaces.Services
{
    public interface IFavoriteService
    {
        Task<ResponseModel<Favorite>> DeleteFavoriteAsync(Guid clientId, Guid productId);
        Task<ResponseModel<List<Favorite>>> GetAllClientFavoritesAsync(Guid clientId);
        Task<ResponseModel<Favorite>> InsertFavoriteAsync(FavoriteViewModel favoriteViewModel);
    }
}
