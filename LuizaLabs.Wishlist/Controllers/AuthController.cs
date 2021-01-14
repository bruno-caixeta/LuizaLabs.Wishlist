using LuizaLabs.Wishlist.API.Util;
using LuizaLabs.Wishlist.Domain.Entities;
using LuizaLabs.Wishlist.Domain.Repositories.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace LuizaLabs.Wishlist.API.Controllers
{
    /// <summary>
    /// Authentication Controller
    /// </summary>
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public IActionResult Authenticate([FromBody] User model)
        {
            var user = UserRepository.GetUser(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return Ok(new { user = user, token = token });
        }
    }
}
