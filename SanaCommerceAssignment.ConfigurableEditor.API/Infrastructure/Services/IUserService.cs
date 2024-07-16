using SanaCommerceAssignment.ConfigurableEditor.API.Models;
using SanaCommerceAssignment.ConfigurableEditor.Shared.DTOs.Users;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Enums;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Requests.Accounts;
namespace SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Services;
public interface IUserService
{
    Task<ServiceResult> AuthenticateAsync(AuthenticateUser user);
}

public class UserService : IUserService
{
    private readonly ITokenService _tokenService;
    private readonly IEnumerable<BaseDto> _UsersList;
    public UserService(ITokenService tokenService)
    {
        _tokenService = tokenService;
        _UsersList = new List<BaseDto>() { new("client@abc.com", "Password", UserTypeEnum.Client), new("admin@abc.com", "Password", UserTypeEnum.Admin), };
    }
    public async Task<ServiceResult> AuthenticateAsync(AuthenticateUser user)
    {
        var userCollection = _UsersList.Where(u => u.Username == user.Username && u.Password == user.Password).ToList();
        if (!userCollection.Any())
            return new(false, "Invalid credentials");

        var authenticatedUser = userCollection.First();

        var token = await _tokenService.GetTokenAsync(authenticatedUser.Username, authenticatedUser.Type);
        var tokenDto = (TokenDto)token.Obj!;
        return new(true, "Authenticated", tokenDto);
    }
}