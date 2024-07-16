using SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Constants;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Extensions;
using SanaCommerceAssignment.ConfigurableEditor.Shared.DTOs.Users;
namespace SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Services;
public class IHttpService
{
    protected HttpClient _client;
    protected IHttpContextAccessor _httpContextAccessor;
    public IHttpService(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected void AuthenticateRequest()
    {
        var token = (_httpContextAccessor.HttpContext!.Session.GetObjectFromJson<TokenDto>(SessionConstants.Token)).Token;
        _client.DefaultRequestHeaders.Add("Authorization", token);
    }
}
