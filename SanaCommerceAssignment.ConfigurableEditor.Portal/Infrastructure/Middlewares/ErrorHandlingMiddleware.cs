using System.Net;
namespace SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Middlewares;
public class ErrorHandlingMiddleware(RequestDelegate requestDelegate)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await requestDelegate(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }
    private static Task HandleException(HttpContext context, Exception ex)
    {
        if (ex is UnauthorizedAccessException)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 401;
            context.Response.Redirect("/Accounts/Login", true);
            return Task.CompletedTask;
        }
        var requestId = context.TraceIdentifier;
        var method = context.Request.Method;
        var path = context.Request.Path;

        var errorMessage = Newtonsoft.Json.JsonConvert.SerializeObject(
            new
            {
                RequestId = requestId,
                Message = "Internal Server Error",
                Code = HttpStatusCode.InternalServerError,
                Exception = ex.Message,
                InnerException = ex.InnerException
            });

        context.Response.WriteAsJsonAsync(errorMessage);
        return Task.CompletedTask;
    }
}

