using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace TestDokerGrpc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController(IDatabase redis) : Controller
    {
        private readonly IDatabase _redis = redis;
        [HttpGet]
        public async Task<string> Get(int id)
        {
            var value = await _redis.StringGetAsync($"user:{id}");
            if (value.HasValue)
                return value;
            return "Тебя нету";
        }
    }
}
