using SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Middlewares;
namespace SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Startup;
public static class MiddlewareConfiguration
{
    public static WebApplication ConfigureMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}
