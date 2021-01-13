using LuizaLabs.Wishlist.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace LuizaLabs.Wishlist.Domain.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProduct(Guid productId);
    }
}
