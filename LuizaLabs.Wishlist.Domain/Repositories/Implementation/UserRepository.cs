using LuizaLabs.Wishlist.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LuizaLabs.Wishlist.Domain.Repositories.Implementation
{
    public static class UserRepository
    {
        public static User GetUser(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { UserId = Guid.Parse("ac4aa3d2-9949-4729-b1ba-87a7542f928e"), Username = "green", Password = "go", Role = "manager" });
            users.Add(new User { UserId = Guid.Parse("b0f84b7a-e4cf-4a32-a64f-f07cf7fa2cbd"), Username = "red", Password = "stop", Role = "employee" });
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }
    }
}
