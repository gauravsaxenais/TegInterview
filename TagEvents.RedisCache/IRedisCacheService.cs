namespace TagEvents.RedisCache
{
    /// <summary>
    /// IRedisCacheService Encapsulates IDistributedCache functionality.
    /// </summary>
    public interface IRedisCacheService
    {
        T Get<T>(string key);
        Task<T> GetAsync<T>(string key);
        void Set(string key, object data, int cacheTimeInMinutes);
        Task SetAsync(string key, object data, int cacheTimeInMinutes);
        T GetOrSet<T>(string key, Func<T> factory, int cacheTimeInMinutes);
        void Remove(string key);
        Task RemoveAsync(string key);
        bool TryGetValue<T>(string key, out T result);
    }
}