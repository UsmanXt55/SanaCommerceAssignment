using Newtonsoft.Json;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Constants;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Models;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Models.ViewModels;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Enums;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Requests.Templates;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Response.Templates;
using System.Text;
namespace SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Services;
public interface ITemplateService
{
    Task<ServiceResult> GetAsync(string pageId, CancellationToken cancellationToken);
    Task<ServiceResult> PostAsync(string pageId, List<FieldViewModel> fields, CancellationToken cancellationToken);
}

public class TemplateService : ITemplateService
{
    private readonly HttpClient _client;
    public TemplateService(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient(ApiClientConstants.Server);
    }

    public async Task<ServiceResult> GetAsync(string pageId, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _client.GetAsync(string.Format("api/template/{0}", pageId), cancellationToken);
            if (!response.IsSuccessStatusCode)
                return new(false, "Failed");

            var rawString = await response.Content.ReadAsStringAsync();
            var get200Result = JsonConvert.DeserializeObject<Get200>(rawString)!;

            return new(true, "Data fetched", get200Result.Fields);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ServiceResult> PostAsync(string pageId, List<FieldViewModel> fields, CancellationToken cancellationToken)
    {
        try
        {
            List<CreateTemplateRequestField> requestFields = new();
            fields.ForEach(field =>
            {
                requestFields.Add(new(field.Name, field.Name, (FieldTypeEnum)Enum.Parse(typeof(FieldTypeEnum), field.Type) ));
            });

            CreateTemplateRequest request = new(pageId, requestFields);
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/template", content, cancellationToken);
            if (!response.IsSuccessStatusCode)
                return new(false, "Failed");

            return new(true, "Data posted");
        }
        catch (Exception)
        {
            throw;
        }
    }
}