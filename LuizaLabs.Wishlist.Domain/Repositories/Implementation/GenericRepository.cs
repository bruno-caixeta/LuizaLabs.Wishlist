using LuizaLabs.Wishlist.Domain.Database;
using LuizaLabs.Wishlist.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuizaLabs.Wishlist.Domain.Repositories.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public Task<int> Delete(T entity)
        {
            using (var db = new PostgresContext())
            {
                db.Set<T>();
                db.Remove(entity);
                return db.SaveChangesAsync();
            }
        }

        public Task<List<T>> GetAll()
        {
            using (var db = new PostgresContext())
            {
                return db.Set<T>().ToListAsync();
            }
        }

        public Task<T> GetOne(Guid id)
        {
            using (var db = new PostgresContext())
            {
                return db.Set<T>().FindAsync(id).AsTask();
            }
        }

        public Task<int> Insert(T entity)
        {
            using (var db = new PostgresContext())
            {
                db.Set<T>();
                db.Add(entity);
                return db.SaveChangesAsync();
            }
        }

        public Task<int> Update(T entity)
        {
            using (var db = new PostgresContext())
            {
                db.Set<T>();
                db.Update(entity);
                return db.SaveChangesAsync();
            }
        }
    }
}
