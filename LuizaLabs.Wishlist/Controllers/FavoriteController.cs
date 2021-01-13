using LuizaLabs.Wishlist.App.Interfaces.Services;
using LuizaLabs.Wishlist.App.Interfaces.Wrappers;
using LuizaLabs.Wishlist.Domain.Entities;
using LuizaLabs.Wishlist.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LuizaLabs.Wishlist.API.Controllers
{
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly ILoggerWrapper<Favorite> _logger;
        private IFavoriteService _service;

        public FavoriteController(ILoggerWrapper<Favorite> logger, IFavoriteService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> InsertFavoriteAsync([FromBody] FavoriteViewModel clientViewModel)
        {
            var result = await _service.InsertFavoriteAsync(clientViewModel);

            if (result.error)
                return StatusCode((int)result.errorInfo?.FirstOrDefault().statusCode, result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientFavoritesAsync(Guid clientId)
        {
            var result = await _service.GetAllClientFavoritesAsync(clientId);

            if (result.error)
                return StatusCode((int)result.errorInfo?.FirstOrDefault().statusCode, result);

            return Ok(result);
        }

        [HttpDelete("{clientId}/{productId}")]
        public async Task<IActionResult> DeleteFavoriteAsync(Guid clientId, Guid productId)
        {
            var result = await _service.DeleteFavoriteAsync(clientId, productId);

            if (result.error)
                return StatusCode((int)result.errorInfo?.FirstOrDefault().statusCode, result);

            return Ok(result);
        }
    }
}
