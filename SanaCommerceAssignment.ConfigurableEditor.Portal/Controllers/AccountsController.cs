using Microsoft.AspNetCore.Mvc;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Constants;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Extensions;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Services;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Models;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Models.ViewModels;
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
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = accountsService.AuthenticateUser(model);
        if (!result.Success)
        {

        }

        var account = (AccountsModel)result.Obj!;
        HttpContext.Session.SetObjectAsJson(SessionConstants.Token, account);
        if (account.Type == UserTypeEnum.Client)
            return RedirectToAction("Index", "Home");
        else
            return RedirectToAction("Admin", "Home");
    }
}