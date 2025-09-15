using StackExchange.Redis;
using System.Threading.Tasks;

namespace TestDokerGrpc
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var redisConnectionString = builder.Configuration["Redis:ConnectionString"] ?? "redis:6379,abortConnect=false,connectTimeout=5000";
            await Task.Delay(5000);
            //redisConnectionString = "redis:6379,abortConnect=false,connectTimeout=5000,connectRetry=5";
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            var redis = ConnectionMultiplexer.Connect(redisConnectionString);
            builder.Services.AddSingleton<IConnectionMultiplexer>(redis);
            builder.Services.AddScoped<IDatabase>(sp =>
                sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());
            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
