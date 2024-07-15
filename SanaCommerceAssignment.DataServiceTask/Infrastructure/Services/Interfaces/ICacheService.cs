namespace SanaCommerceAssignment.DataServiceTask.Infrastructure.Services.Interfaces;
public interface ICacheService<T>
{
    T? Get(string cacheKey);
    void Set(string cacheKey, T value, TimeSpan expiration);
}
