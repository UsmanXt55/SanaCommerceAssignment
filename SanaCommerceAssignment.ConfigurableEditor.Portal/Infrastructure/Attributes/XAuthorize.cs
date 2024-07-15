using Microsoft.AspNetCore.Mvc.Filters;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Constants;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Extensions;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Models;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Enums;
namespace SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Attributes;
public class XAuthorize(params UserTypeEnum[] roles) : Attribute, IAsyncAuthorizationFilter
{

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var tokenSession = context.HttpContext.Session.GetObjectFromJson<AccountsModel>(SessionConstants.Token);
        if (tokenSession is null)
            throw new UnauthorizedAccessException("Token not found");

        if (roles.Length > 0)
        {
            if (!roles.Contains(tokenSession.Type))
                throw new UnauthorizedAccessException("Unauthorized");
        }

        await Task.CompletedTask;
    }
}