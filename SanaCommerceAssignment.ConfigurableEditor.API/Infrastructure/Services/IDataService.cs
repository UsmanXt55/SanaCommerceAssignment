using SanaCommerceAssignment.ConfigurableEditor.API.DataContext.Repositories;
using SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Extensions;
using SanaCommerceAssignment.ConfigurableEditor.API.Models;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Requests.Data;
namespace SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Services;
public interface IDataService
{
    Task<ServiceResult> PostAsync(List<CreateData> request, CancellationToken cancellationToken);
}

public class DataService(
    ITemplateRepository templateRepository) : IDataService
{
    public async Task<ServiceResult> PostAsync(List<CreateData> request, CancellationToken cancellationToken)
    {
        foreach (var data in request)
        {
            var templateFieldCollection = await templateRepository.QueryAsync(new(
                data.PageId,
                data.FieldId), cancellationToken);
            if (!templateFieldCollection.Any())
                return new(false, $"Field '{data.FieldId}' is an unknown field");

            var field = templateFieldCollection.First();
            var isValidValue = field.Type.ValidateValue(data.Value);
            if(!isValidValue)
                return new(false, $"Field '{data.FieldId}' has invalid formatted value is passed.");
        }

        return new(true, "Data has been posted");
    }
}