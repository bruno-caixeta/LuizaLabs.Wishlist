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
    public class ClientService : IClientService
    {
        private readonly ILoggerWrapper<ClientService> _logger;
        private IClientRepository _repository;
        private readonly ClientResponseMessages _messages;

        public ClientService(ILoggerWrapper<ClientService> logger, IClientRepository repository, ClientResponseMessages messages)
        {
            _logger = logger;
            _repository = repository;
            _messages = messages;
        }


        public async Task<ResponseModel<Client>> DeleteClientAsync(Guid clientId)
        {
            try
            {
                var targetClient = await _repository.GetOne(clientId);

                if (targetClient is null)
                {
                    return new ResponseModel<Client>(true, new List<ErrorInfo>
                    {
                        new ErrorInfo(HttpStatusCode.NotFound, _messages.ClientNotFound,
                            string.Format(_messages.ClientNotFoundDescription, clientId))
                    });
                }

                var deleted = await _repository.Delete(targetClient);
                if (deleted == 0)
                {
                    return new ResponseModel<Client>(true, new List<ErrorInfo>
                    {
                        new ErrorInfo(HttpStatusCode.Conflict, _messages.NoClientChanged, _messages.NoClientChangedDescription)
                    });
                }
                return new ResponseModel<Client>(targetClient);
            }
            catch(Exception ex)
            {
                var message = string.Format(_messages.InternalErrorDescription, ex.Message);
                _logger.LogError(ex, message);

                return new ResponseModel<Client>(true, new List<ErrorInfo>
                {
                    new ErrorInfo(HttpStatusCode.InternalServerError, _messages.InternalError, message)
                });
            }
        }

        public async Task<ResponseModel<List<Client>>> GetAllClientsAsync()
        {
            try
            {
                var clients = await _repository.GetAll();

                if (!clients.Any())
                {
                    return new ResponseModel<List<Client>>(true, new List<ErrorInfo>
                {
                    new ErrorInfo(HttpStatusCode.NotFound, _messages.ClientNotFound,
                        string.Format(_messages.ClientNotFoundDescription, Guid.Empty))
                });
                }

                return new ResponseModel<List<Client>>(clients);
            }
            catch (Exception ex)
            {
                var message = string.Format(_messages.InternalErrorDescription, ex.Message);
                _logger.LogError(ex, message);

                return new ResponseModel<List<Client>>(true, new List<ErrorInfo>
                {
                    new ErrorInfo(HttpStatusCode.InternalServerError, _messages.InternalError, message)
                });
            }

        }

        public async Task<ResponseModel<Client>> GetClientAsync(Guid clientId)
        {
            try
            {
                var client = await _repository.GetOne(clientId);

                if (client is null)
                {
                    return new ResponseModel<Client>(true, new List<ErrorInfo>
                    {
                        new ErrorInfo(HttpStatusCode.NotFound, _messages.ClientNotFound,
                            string.Format(_messages.ClientNotFoundDescription, clientId))
                    });
                }
                return new ResponseModel<Client>(client);
            }
            catch (Exception ex)
            {
                var message = string.Format(_messages.InternalErrorDescription, ex.Message);
                _logger.LogError(ex, message);

                return new ResponseModel<Client>(true, new List<ErrorInfo>
                {
                    new ErrorInfo(HttpStatusCode.InternalServerError, _messages.InternalError, message)
                });
            }
        }

        public async Task<ResponseModel<Client>> InsertClientAsync(ClientViewModel clientViewModel)
        {
            try
            {
                var client = new Client(clientViewModel);
                var inserted = await _repository.Insert(client);

                if (inserted == 0)
                {
                    return new ResponseModel<Client>(true, new List<ErrorInfo>
                    {
                        new ErrorInfo(HttpStatusCode.Conflict, _messages.NoClientChanged, _messages.NoClientChangedDescription)
                    });
                }

                return new ResponseModel<Client>(client);
            }
            catch (Exception ex)
            {
                var message = string.Format(_messages.InternalErrorDescription, ex.Message);
                _logger.LogError(ex, message);

                return new ResponseModel<Client>(true, new List<ErrorInfo>
                {
                    new ErrorInfo(HttpStatusCode.InternalServerError, _messages.InternalError, message)
                });
            }
        }

        public async Task<ResponseModel<Client>> UpdateClientAsync(Guid clientId, ClientViewModel clientViewModel)
        {
            try
            {
                var targetClient = await _repository.GetOne(clientId);

                if (targetClient is null)
                {
                    return new ResponseModel<Client>(true, new List<ErrorInfo>
                    {
                        new ErrorInfo(HttpStatusCode.NotFound, _messages.ClientNotFound,
                            string.Format(_messages.ClientNotFoundDescription, clientId))
                    });
                }

                targetClient.UpdateClientInstance(clientViewModel);
                var updated = await _repository.Update(targetClient);

                if (updated == 0)
                {
                    return new ResponseModel<Client>(true, new List<ErrorInfo>
                    {
                        new ErrorInfo(HttpStatusCode.Conflict, _messages.NoClientChanged, _messages.NoClientChangedDescription)
                    });
                }
                return new ResponseModel<Client>(targetClient);
            }
            catch (Exception ex)
            {
                var message = string.Format(_messages.InternalErrorDescription, ex.Message);
                _logger.LogError(ex, message);

                return new ResponseModel<Client>(true, new List<ErrorInfo>
                {
                    new ErrorInfo(HttpStatusCode.InternalServerError, _messages.InternalError, message)
                });
            }
        }
    }
}
