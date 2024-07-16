using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Services;
using SanaCommerceAssignment.ConfigurableEditor.Shared.DTOs.Users;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Requests.Accounts;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Response;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Response.Accounts;
namespace SanaCommerceAssignment.ConfigurableEditor.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountsController(
    IUserService userService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Authenticate(AuthenticateUser request)
    {
        var result = await userService.AuthenticateAsync(request);
        if (!result.Success)
            return BadRequest(new ApiResponse400BadRequest("Invalid credentials"));

        var token = (TokenDto)result.Obj!;
        return Ok(new Get200(token));
    }
}
