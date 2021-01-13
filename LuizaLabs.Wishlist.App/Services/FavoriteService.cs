using LuizaLabs.Wishlist.App.Interfaces.Services;
using LuizaLabs.Wishlist.App.Interfaces.Wrappers;
using LuizaLabs.Wishlist.App.ResponseModels;
using LuizaLabs.Wishlist.Domain.Entities;
using LuizaLabs.Wishlist.Domain.Repositories.Interfaces;
using LuizaLabs.Wishlist.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LuizaLabs.Wishlist.App.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly ILoggerWrapper<FavoriteService> _logger;
        private IFavoriteRepository _favoriteRepository;
        private IProductRepository _productRepository;
        private readonly FavoriteResponseMessages _messages;

        public async Task<ResponseModel<Favorite>> DeleteFavoriteAsync(Guid clientId, Guid productId)
        {
            var targetFavorite = await _favoriteRepository.GetFavorite(clientId, productId);
            if (targetFavorite is null)
            {
                return new ResponseModel<Favorite>(true, new List<ErrorInfo>
                {
                    new ErrorInfo(HttpStatusCode.NotFound, _messages.NoFavoriteFound,
                    string.Format(_messages.NoFavoriteFoundDescription, clientId, productId))
                });
            }

            var deleted = await _favoriteRepository.Delete(targetFavorite);

            if (deleted == 0)
            {
                return new ResponseModel<Favorite>(true, new List<ErrorInfo>
                {
                    new ErrorInfo(HttpStatusCode.Conflict, _messages.NoFavoriteChanged, _messages.NoFavoriteChangedDescription)
                });
            }

            return new ResponseModel<Favorite>(targetFavorite);
        }

        public async Task<ResponseModel<List<Favorite>>> GetAllClientFavoritesAsync(Guid clientId)
        {
            var favorites = await _favoriteRepository.GetAllClientFavorites(clientId);

            if (!favorites.Any())
            {
                return new ResponseModel<List<Favorite>>(true, new List<ErrorInfo>
                {
                    new ErrorInfo(HttpStatusCode.NotFound, _messages.NoFavoritesFound,
                    string.Format(_messages.NoFavoritesFoundDescription, clientId))
                });
            }

            return new ResponseModel<List<Favorite>>(favorites);
        }

        public async Task<ResponseModel<Favorite>> InsertFavoriteAsync(FavoriteViewModel favoriteViewModel)
        {
            var product = await _productRepository.GetProduct(favoriteViewModel.ProductId);
            if (product is null)
            {
                return new ResponseModel<Favorite>(true, new List<ErrorInfo>
                {
                    new ErrorInfo(HttpStatusCode.NotFound, _messages.ProductDoesNotExist,
                    string.Format(_messages.ProductDoesNotExistDescription, favoriteViewModel.ProductId))
                });
            }

            Favorite favorite = new Favorite(favoriteViewModel);
            var inserted = await _favoriteRepository.Insert(favorite);

            if (inserted == 0)
            {
                return new ResponseModel<Favorite>(true, new List<ErrorInfo>
                {
                    new ErrorInfo(HttpStatusCode.Conflict, _messages.NoFavoriteChanged, _messages.NoFavoriteChangedDescription)
                });
            }

            return new ResponseModel<Favorite>(favorite);
        }
    }
}
