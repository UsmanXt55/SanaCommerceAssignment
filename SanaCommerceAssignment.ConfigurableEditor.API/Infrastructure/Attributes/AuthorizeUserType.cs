using Microsoft.AspNetCore.Mvc.Filters;
using SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Extensions;
using SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Helpers;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Enums;
namespace SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Attributes;
public class AuthorizeUserType(params UserTypeEnum[] userTypes) : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.GetHeaderValue("Authorization");
        if (string.IsNullOrWhiteSpace(token))
            throw new UnauthorizedAccessException("Invalid token");
        token = token.Replace("Bearer ", "");

        var tokenDto = TokenHelper.GetClaims(token);
        if (tokenDto is null)
            throw new UnauthorizedAccessException();

        bool userTypeExist = userTypes.Any(x => x == tokenDto.Type);
        if (!userTypeExist)
            throw new UnauthorizedAccessException($"User of type: {tokenDto.Type} is not authorized to access this resource");

        context.HttpContext.Items.Add("Token", tokenDto);
        return;

        throw new UnauthorizedAccessException();
    }
}
