using SanaCommerceAssignment.DataServiceTask.Infrastructure.Services.Interfaces;
namespace SanaCommerceAssignment.DataServiceTask.Infrastructure.Middlewares;
public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILoggerService logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            var errorMessage = "Internal Server Error";
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(errorMessage);
        }
    }
}
