using Newtonsoft.Json;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Constants;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Models;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Requests.Data;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Response;
using System.Text;
namespace SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Services;
public interface IDataService
{
    Task<ServiceResult> PostAsync(List<CreateData> dataList, CancellationToken cancellationToken);
}

public class DataService : IHttpService, IDataService
{
    public DataService(
        IHttpClientFactory httpClientFactory,
        IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _client = httpClientFactory.CreateClient(ApiClientConstants.Server);
    }

    public async Task<ServiceResult> PostAsync(List<CreateData> dataList, CancellationToken cancellationToken)
    {
        try
        {
            var content = new StringContent(JsonConvert.SerializeObject(dataList), Encoding.UTF8, "application/json");
            AuthenticateRequest();
            var response = await _client.PostAsync("api/data", content, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                var ss = await response.Content.ReadAsStringAsync();
                var badRequestResponse = JsonConvert.DeserializeObject<ApiResponse400BadRequest>(ss)!;
                return new(false, badRequestResponse.Message);
            }

            return new(true, "Data posted");
        }
        catch (Exception)
        {
            throw;
        }
    }
}