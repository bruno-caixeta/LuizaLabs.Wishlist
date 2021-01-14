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
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILoggerWrapper<Client> _logger;
        private IClientService _service;

        public ClientController(ILoggerWrapper<Client> logger, IClientService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> InsertClientAsync([FromBody] ClientViewModel clientViewModel)
        {
            var result = await _service.InsertClientAsync(clientViewModel);

            if (result.error)
                return StatusCode((int)result.errorInfo?.FirstOrDefault().statusCode, result);

            return Ok(result);
        }

        [HttpGet("{clientId}")]
        [Authorize]
        public async Task<IActionResult> GetClientByIdAsync(Guid clientId)
        {
            var result = await _service.GetClientAsync(clientId);

            if (result.error)
                return StatusCode((int)result.errorInfo?.FirstOrDefault().statusCode, result);

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllClientsAsync()
        {
            var result = await _service.GetAllClientsAsync();

            if (result.error)
                return StatusCode((int)result.errorInfo?.FirstOrDefault().statusCode, result);

            return Ok(result);
        }

        [HttpPut("{clientId}")]
        [Authorize]
        public async Task<IActionResult> UpdateClientAsync(Guid clientId, [FromBody] ClientViewModel clientViewModel)
        {
            var result = await _service.UpdateClientAsync(clientId, clientViewModel);

            if (result.error)
                return StatusCode((int)result.errorInfo?.FirstOrDefault().statusCode, result);

            return Ok(result);
        }

        [HttpDelete("{clientId}")]
        [Authorize]
        public async Task<IActionResult> DeleteClientAsync(Guid clientId)
        {
            var result = await _service.DeleteClientAsync(clientId);

            if (result.error)
                return StatusCode((int)result.errorInfo?.FirstOrDefault().statusCode, result);

            return Ok(result);
        }
    }
}
