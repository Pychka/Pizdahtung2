using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Diagnostics.Metrics;

namespace ClientsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController(IDatabase redis) : Controller
    {
        private static int counter = 0;
        private static Dictionary<int, string> users = [];
        private readonly IDatabase _redis = redis;
        [HttpGet]
        public async Task<IActionResult> Reg(string name)
        {
            users.Add(counter, name);
            counter++;

            await _redis.StringSetAsync($"user:{counter}", $"{name}", TimeSpan.FromMinutes(10));
            return Ok();
        }
    }
}
