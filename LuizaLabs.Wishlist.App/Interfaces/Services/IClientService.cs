using LuizaLabs.Wishlist.App.ResponseModels;
using LuizaLabs.Wishlist.Domain.Entities;
using LuizaLabs.Wishlist.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuizaLabs.Wishlist.App.Interfaces.Services
{
    public interface IClientService
    {
        Task<ResponseModel<Client>> DeleteClientAsync(Guid clientId);
        Task<ResponseModel<Client>> GetClientAsync(Guid clientId);
        Task<ResponseModel<List<Client>>> GetAllClientsAsync();
        Task<ResponseModel<Client>> InsertClientAsync(ClientViewModel clientViewModel);
        Task<ResponseModel<Client>> UpdateClientAsync(Guid id, ClientViewModel clientViewModel);
    }
}
