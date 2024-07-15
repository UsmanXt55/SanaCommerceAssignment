using SanaCommerceAssignment.DataServiceTask.Infrastructure.Services.Interfaces;
namespace SanaCommerceAssignment.DataServiceTask.Infrastructure.ServiceDecorators;
public class CachingDataServiceDecorator(
    IDataService dataService,
    ICacheService<IEnumerable<string>> cache) : IDataService
{
    private readonly string __CacheKey = "DataServiceCache";
    public IEnumerable<string> GetLines()
    {
        var cachedData = cache.Get(__CacheKey);
        if (cachedData is not null)
          return cachedData;

        var data = dataService.GetLines();
        cache.Set(__CacheKey, data, TimeSpan.FromMinutes(1));
        return data;
    }
}
