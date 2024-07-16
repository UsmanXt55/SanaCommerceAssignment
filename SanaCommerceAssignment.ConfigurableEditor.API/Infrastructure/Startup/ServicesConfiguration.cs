using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SanaCommerceAssignment.ConfigurableEditor.API.DataContext;
using SanaCommerceAssignment.ConfigurableEditor.API.DataContext.Repositories;
using SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Constants;
using SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Mappers;
using SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Services;
using System.Text;
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

        // ***************************  AUTHENTICATION  ****************************
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = TokenConstants.Issuer,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenConstants.SecurityKey)),
                      ValidAudience = TokenConstants.Audience
                  };
              });
        // ***************************  AUTHENTICATION  ****************************


        builder.Services.AddTransient<TemplateMapper>();
        builder.Services.AddTransient<ITemplateRepository, TemplateRepository>();
        builder.Services.AddTransient<ITemplateService, TemplateService>();
        builder.Services.AddTransient<IDataService, DataService>();
        builder.Services.AddTransient<ITokenService, TokenService>();
        builder.Services.AddTransient<IUserService, UserService>();

        return builder;
    }

}
