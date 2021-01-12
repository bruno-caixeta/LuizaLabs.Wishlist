using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuizaLabs.Wishlist.Domain.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<int> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
        Task<T> GetOne(Guid id);
        Task<List<T>> GetAll();
    }
}
