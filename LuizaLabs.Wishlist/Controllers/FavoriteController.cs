using LuizaLabs.Wishlist.App.Interfaces.Services;
using LuizaLabs.Wishlist.App.Interfaces.Wrappers;
using LuizaLabs.Wishlist.Domain.Entities;
using LuizaLabs.Wishlist.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LuizaLabs.Wishlist.API.Controllers
{
    /// <summary>
    /// Favorite Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FavoriteController : ControllerBase
    {
        private readonly ILoggerWrapper<Favorite> _logger;
        private IFavoriteService _service;

        public FavoriteController(ILoggerWrapper<Favorite> logger, IFavoriteService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Save product in wishlist
        /// </summary>
        /// <param name="favoriteViewModel"></param>
        /// <returns>Added object</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertFavoriteAsync([FromBody] FavoriteViewModel favoriteViewModel)
        {
            var result = await _service.InsertFavoriteAsync(favoriteViewModel);

            if (result.error)
                return StatusCode((int)result.errorInfo?.FirstOrDefault().statusCode, result);

            return Ok(result);
        }

        /// <summary>
        /// Get client wishlist with product info
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet("{clientId}")]
        [Authorize]
        public async Task<IActionResult> GetAllClientFavoritesAsync(Guid clientId)
        {
            var result = await _service.GetAllClientFavoritesAsync(clientId);

            if (result.error)
                return StatusCode((int)result.errorInfo?.FirstOrDefault().statusCode, result);

            return Ok(result);
        }

        /// <summary>
        /// Remove product from wishlist
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("{clientId}/{productId}")]
        [Authorize]
        public async Task<IActionResult> DeleteFavoriteAsync(Guid clientId, Guid productId)
        {
            var result = await _service.DeleteFavoriteAsync(clientId, productId);

            if (result.error)
                return StatusCode((int)result.errorInfo?.FirstOrDefault().statusCode, result);

            return Ok(result);
        }
    }
}
