using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text.Json;

namespace TagEvents.RedisCache
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly ConnectionMultiplexer _redisMultiplexer;
        private readonly IConfiguration _configuration;

        public RedisCacheService(IConfiguration configuration)
        {
            _configuration = configuration;
            var options = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                EndPoints = { _configuration["RedisConnectionString"] }
            };
            _redisMultiplexer = ConnectionMultiplexer.Connect(options);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var db = _redisMultiplexer.GetDatabase();
            string? result = await db.StringGetAsync(key);
            if (!string.IsNullOrEmpty(result))
            {
                return JsonSerializer.Deserialize<T>(result);
            }
            return await Task.FromResult<T?>(default);
        }

        public async Task<bool> SetAsync<T>(string key, T obj)
        {
            var db = _redisMultiplexer.GetDatabase();
            return await db.StringSetAsync(key, JsonSerializer.Serialize(obj));
        }

        public async Task<bool> RemoveAsync(string key)
        {
            var db = _redisMultiplexer.GetDatabase();
            return await db.KeyDeleteAsync(key);
        }
    }
}
