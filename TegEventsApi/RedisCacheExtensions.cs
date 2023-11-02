using Microsoft.Extensions.Caching.Redis;
using TagEvents.RedisCache;

namespace TegEventsApi
{
    public static class RedisCacheExtensions
    {

        /// <summary>
        /// Adds IRedisCacheService to IServiceCollection.
        /// </summary>
        public static IServiceCollection AddRedisCache(this IServiceCollection services, Action<RedisCacheOptions> setupAction)
        {
            services.AddDistributedRedisCache(setupAction);

            services.AddSingleton<IRedisCacheService, RedisCacheService>();
            return services;
        }
    }
}
