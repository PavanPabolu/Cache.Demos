using Cache.Distributed.Redis.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace Cache.Distributed.Redis.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //private readonly AdventureWorksDbContext _context;
        private readonly IDistributedCache _cache;

        public ProductController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cacheKey = "KEY1";
            List<Product1> products = new List<Product1>();

            // Get data from cache
            var cachedData = await _cache.GetAsync(cacheKey);
            if (cachedData != null)
            {
                // If data found in cache, encode and deserialize cached data
                var cachedDataString = Encoding.UTF8.GetString(cachedData);
                products = JsonConvert.DeserializeObject<List<Product1>>(cachedDataString);
            }
            else
            {
                // If not found, then fetch data from database
                //products = await _context.Products.ToListAsync();
                products = GetDummyData();

                // serialize data
                var cachedDataString = JsonConvert.SerializeObject(products);
                var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                // set cache options 
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(2))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                // Add data in cache
                await _cache.SetAsync(cacheKey, newDataToCache, options);
            }

            return Ok(products);
        }

        private List<Product1> GetDummyData()
        {
            return new List<Product1>()
            {
                new Product1() { ProductId = 1, Name="iPhone", ListPrice=500 },
                new Product1() { ProductId = 2, Name="Samsung", ListPrice=400 },
                new Product1() { ProductId = 3, Name="Moto", ListPrice=300 }
            };
        }
    }
}
