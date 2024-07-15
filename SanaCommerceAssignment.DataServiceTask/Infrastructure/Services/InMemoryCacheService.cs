using Microsoft.Extensions.Caching.Memory;
using SanaCommerceAssignment.DataServiceTask.Infrastructure.Services.Interfaces;
namespace SanaCommerceAssignment.DataServiceTask.Infrastructure.Services;
public class InMemoryCacheService<T>(
    IMemoryCache memoryCache) : ICacheService<T>
{
    public T? Get(string cacheKey)
    {
        var cachedValue = memoryCache.Get<T?>(cacheKey);
        return cachedValue;
    }

    public void Set(string cacheKey, T value, TimeSpan expiration)
    {
        memoryCache.Set(cacheKey, value, expiration);
    }
}
