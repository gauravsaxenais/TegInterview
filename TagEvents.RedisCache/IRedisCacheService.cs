namespace TagEvents.RedisCache
{
    /// <summary>
    /// IRedisCacheService Encapsulates IDistributedCache functionality.
    /// </summary>
    public interface IRedisCacheService
    {
        Task<T?> GetAsync<T>(string key);
        Task<bool> SetAsync<T>(string key, T obj);
        Task<bool> RemoveAsync(string key);
    }
}