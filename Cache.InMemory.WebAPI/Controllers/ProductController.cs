using Cache.InMemory.WebAPI.Data;
using Cache.InMemory.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Cache.InMemory.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AdventureWorksDbContext _context;
        private readonly IMemoryCache _cache;

        public ProductController(AdventureWorksDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var products = await _context.Products.ToListAsync();
            //return Ok(products);

            var cacheKey = "GET_ALL_PRODUCTS";

            // If data found in cache, return cached data
            if(_cache.TryGetValue(cacheKey, out List<Product> products))
                return Ok(products);

            // If not found, then fetch data from database
            products = await _context.Products.ToListAsync();

            // Add data in cache
            //_cache.Set(cacheKey, products);

            //AbsoluteExpiration: To cache data for the exact time, we can use the AbsoluteExpiration setting. In the following code snippet, the AbsoluteExpiration is set to 5 minutes, which means no matter how frequently our cached data is accessed, it will flush after 5 minutes.
            //SlidingExpiration: Using the SlidingExpiration setting which allows us to remove cached items which are not frequently accessed. In the example below, I set it to 5 minutes which means that data will remove from the cache only if it is not accessed in the last 5 minutes.
            //Both: If our data is accessed more frequently than our sliding expiration time, then we will end up in a situation where our data will never expire. Application users will never see fresh data fetched from the database. We can solve this problem by combing both sliding and absolute expiration settings. In the following code snippet, the application will keep on serving the cached data to users if they are accessing the data frequently within 5 minute expiry time but due to absolute expiration set to 60 minutes, the fresh data will replace the old cached data every 60 minutes.
            var cacheOptions = new MemoryCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromMinutes(5),
                AbsoluteExpiration = DateTime.Now.AddMinutes(60)
                //Priority = CacheItemPriority.High
            };

            _cache.Set(cacheKey, products, cacheOptions);



            return Ok(products);
        }
    }
}
