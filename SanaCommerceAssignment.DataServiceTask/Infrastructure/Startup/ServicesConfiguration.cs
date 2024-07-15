using Microsoft.Extensions.Caching.Memory;
using SanaCommerceAssignment.DataServiceTask.Infrastructure.ServiceDecorators;
using SanaCommerceAssignment.DataServiceTask.Infrastructure.Services;
using SanaCommerceAssignment.DataServiceTask.Infrastructure.Services.Interfaces;
namespace SanaCommerceAssignment.DataServiceTask.Infrastructure.Startup;
public static class ServicesConfiguration
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        builder.Configuration.AddJsonFile($"appsettings.{env}.json", false, true);
        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<ILoggerService, LoggerService>();
        builder.Services.AddTransient<IDataService>(provider =>
        {
            var filePath = builder.Configuration.GetSection("FilePath").Value!;
            var dataService = new FileDataService(filePath);

            var cacheService = new InMemoryCacheService<IEnumerable<string>>(provider.GetRequiredService<IMemoryCache>());
            var cachingDecorator = new CachingDataServiceDecorator(dataService, cacheService);

            var loggingService = new LoggerService(provider.GetRequiredService<ILogger<LoggerService>>());
            var loggingDecorator = new LoggerDataServiceDecorator(cachingDecorator, loggingService);

            return loggingDecorator;
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpContextAccessor();

       

        return builder;
    }
}