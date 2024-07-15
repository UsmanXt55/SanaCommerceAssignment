using SanaCommerceAssignment.IPRestrictionTask.Infrastructure.Models;
namespace SanaCommerceAssignment.IPRestrictionTask.Infrastructure.Middlewares;
public class IpAddressFilerationMiddleware(RequestDelegate next)
{
    public async Task Invoke(
        HttpContext context,
        IConfiguration configuration)
    {
        var remoteIpAddress = context.Connection.RemoteIpAddress?.ToString();
        var ipAddressFilterationOptions = configuration.GetSection("IPAddressFilteration").Get<IpAddressFilterationConfiguration>()!;
        if (remoteIpAddress is null)
        {
            return;
        }

        if (IsIpBlocked(remoteIpAddress, ipAddressFilterationOptions.BlacklistedIpAddresses))
        {
            context.Response.StatusCode = ipAddressFilterationOptions.StausCode;
            return;
        }

        await next(context);
    }

    private bool IsIpBlocked(string ipAddress, string blackListedIpAddresses)
    {
        return blackListedIpAddresses.Split(',').Contains(ipAddress);
    }
}
