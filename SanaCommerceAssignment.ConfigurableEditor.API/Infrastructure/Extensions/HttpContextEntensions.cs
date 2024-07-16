namespace SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Extensions;
public static class HttpContextEntensions
{
    public static string? GetHeaderValue(this HttpContext httpContext, string headerName)
    {
        if (httpContext.Request.Headers.TryGetValue(headerName, out var values))
            return values.ToString();
        return null;
    }
}
