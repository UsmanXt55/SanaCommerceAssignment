using Microsoft.AspNetCore.Mvc;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Constants;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Extensions;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Services;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Models.ViewModels;
using SanaCommerceAssignment.ConfigurableEditor.Shared.DTOs.Users;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Enums;
namespace SanaCommerceAssignment.ConfigurableEditor.Portal.Controllers;
public class AccountsController(
    IAccountsService accountsService) : BaseController
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result =  await accountsService.AuthenticateUser(model, cancellationToken);
        if (!result.Success)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var token = (TokenDto)result.Obj!;
        HttpContext.Session.SetObjectAsJson(SessionConstants.Token, token);
        if (token.Type == UserTypeEnum.Client)
            return RedirectToAction("Index", "Home");
        else
            return RedirectToAction("Admin", "Home");
    }
}