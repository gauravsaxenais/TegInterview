using TagEvents.RedisCache;

namespace TegEventsApi
{
    public static class RedisCacheExtensions
    {

        /// <summary>
        /// Adds IRedisCacheService to IServiceCollection.
        /// </summary>
        public static IServiceCollection AddRedisCache(this IServiceCollection services)
        {
            services.AddSingleton<IRedisCacheService, RedisCacheService>();
            return services;
        }
    }
}
