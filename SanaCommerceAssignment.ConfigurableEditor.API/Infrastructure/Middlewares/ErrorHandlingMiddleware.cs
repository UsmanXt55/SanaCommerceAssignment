using SanaCommerceAssignment.ConfigurableEditor.Shared.Response;
using System.Text.Json;
namespace SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Middlewares;
public class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            if (ex is UnauthorizedAccessException)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
            }
            else
            {
                var errorMessage = JsonSerializer.Serialize(new ApiResponse500InternalServerError("Internal Server Error"));

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(errorMessage);
            }
        }
    }
}
