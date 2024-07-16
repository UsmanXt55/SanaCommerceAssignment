using Newtonsoft.Json;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Constants;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Models;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Models.ViewModels;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Response;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Response.Accounts;
using System.Text;
namespace SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Services;
public interface IAccountsService
{
    Task<ServiceResult> AuthenticateUser(LoginViewModel model, CancellationToken cancellationToken);
}

public class AccountsService : IAccountsService
{
    private readonly HttpClient _client;
    public AccountsService(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient(ApiClientConstants.Server);
    }

    public async Task<ServiceResult> AuthenticateUser(LoginViewModel model, CancellationToken cancellationToken)
    {
        try
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/accounts", content, cancellationToken);
            var ss = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var badRequestResponse = JsonConvert.DeserializeObject<ApiResponse400BadRequest>(ss)!;
                return new(false, badRequestResponse.Message);
            }

            var token = JsonConvert.DeserializeObject<Get200>(ss)!;
            return new(true, "Authenticated", token.Token);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
