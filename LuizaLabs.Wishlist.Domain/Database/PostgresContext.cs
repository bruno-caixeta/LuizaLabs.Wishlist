using LuizaLabs.Wishlist.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace LuizaLabs.Wishlist.Domain.Database
{
    public class PostgresContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            modelBuilder.HasDefaultSchema("Wishlist");

            modelBuilder.Entity<Favorite>().HasKey(f => new { f.ClientId, f.ProductId });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("PGSQL_CONNECTIONSTRING"));
        }
    }
}
