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
            modelBuilder.Ignore<Product>();

            modelBuilder.Entity<Favorite>().HasIndex("ClientId", "ProductId").IsUnique();
            modelBuilder.Entity<Client>().HasIndex(c => c.Email).IsUnique();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("host=localhost;port=5432;database=Wishlist;user id=postgres;password=postgres;Command Timeout=60");
        }
    }
}
