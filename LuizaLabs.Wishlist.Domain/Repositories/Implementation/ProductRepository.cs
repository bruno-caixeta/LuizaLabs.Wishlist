using LuizaLabs.Wishlist.Domain.Entities;
using LuizaLabs.Wishlist.Domain.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LuizaLabs.Wishlist.Domain.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        static readonly HttpClient httpClient = new HttpClient();
        public async Task<Product> GetProduct(Guid productId)
        {
            var response = await httpClient.GetAsync(string.Format(Environment.GetEnvironmentVariable("PRODUCT_DETAIL_API"), productId));
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            var responseBody = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(responseBody);
            return product;
        }
    }
}
