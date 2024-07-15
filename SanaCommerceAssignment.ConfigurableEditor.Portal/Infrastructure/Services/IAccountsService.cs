using SanaCommerceAssignment.ConfigurableEditor.Portal.Models;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Models.ViewModels;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Enums;
namespace SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Services;
public interface IAccountsService
{
    ServiceResult AuthenticateUser(LoginViewModel model);
}

public class AccountsService : IAccountsService
{
    public ServiceResult AuthenticateUser(LoginViewModel model)
    {
        if (model.Username == "client@abc.com" && model.Password == "Password")
            return new(true, "Authenticated", new AccountsModel(UserTypeEnum.Client));
        else if (model.Username == "admin@abc.com" && model.Password == "Password")
            return new(true, "Authenticated", new AccountsModel(UserTypeEnum.Admin));

        return new(false, "Invalid credentials");
    }
}
