using SanaCommerceAssignment.DataServiceTask.Infrastructure.Middlewares;
namespace SanaCommerceAssignment.DataServiceTask.Infrastructure.Startup;
public static class MiddlewareConfiguration
{
    public static WebApplication ConfigureMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.MapControllers();

        return app;
    }
}