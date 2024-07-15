using Microsoft.EntityFrameworkCore;
using SanaCommerceAssignment.ConfigurableEditor.API.DataContext;
using SanaCommerceAssignment.ConfigurableEditor.API.DataContext.Repositories;
using SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Mappers;
using SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Services;
using System.Text.Json.Serialization;
namespace SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Startup;
public static class ServicesConfiguration
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        builder.Configuration.AddJsonFile($"appsettings.{env}.json", false, true);
        builder.Services.AddDbContext<AppDbContext>(options => options
            .UseSqlite(builder.Configuration.GetConnectionString("AppDb")!)
            .EnableSensitiveDataLogging()
            .UseSnakeCaseNamingConvention(), ServiceLifetime.Transient);

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddTransient<TemplateMapper>();
        builder.Services.AddTransient<ITemplateRepository, TemplateRepository>();
        builder.Services.AddTransient<ITemplateService, TemplateService>();
        builder.Services.AddTransient<IDataService, DataService>();

        return builder;
    }

}
