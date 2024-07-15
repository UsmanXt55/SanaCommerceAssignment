using SanaCommerceAssignment.ConfigurableEditor.API.DataContext.Repositories;
using SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Extensions;
using SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Mappers;
using SanaCommerceAssignment.ConfigurableEditor.API.Models;
using SanaCommerceAssignment.ConfigurableEditor.Shared.DTOs.Templates;
namespace SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Services;
public interface ITemplateService
{
    Task<ServiceResult> GetAsync(string pageId, CancellationToken cancellationToken);
    Task<ServiceResult> CreateAsync(IEnumerable<BaseDto> dtoList, CancellationToken cancellationToken);
}

public class TemplateService(
    ITemplateRepository repository,
    TemplateMapper mapper) : ITemplateService
{
    public async Task<ServiceResult> GetAsync(string pageId, CancellationToken cancellationToken)
    {
        var collection = await repository.QueryAsync(new(pageId, null), cancellationToken);
        if (!collection.Any())
            return new(false, "Template not found");


        List<GetDto> fields = new();
        collection.ToList().ForEach(coll =>
        {
            fields.Add(new(coll.Id, coll.PageId, coll.FieldId, coll.FieldTitle, coll.Type, coll.Type.GetInputType(), coll.Type.GetInputReadonly()));
        });

        return new(true, "Template retreived", fields);
    }
    public async Task<ServiceResult> CreateAsync(IEnumerable<BaseDto> dtoList, CancellationToken cancellationToken)
    {
        foreach (var dto in dtoList.ToList())
        {
            var collection = await repository.QueryAsync(new(dto.PageId, dto.FieldId), cancellationToken);
            if (!collection.Any())
            {
                var entity = mapper.MapToEntity(dto);
                await repository.CreateAsync(entity, cancellationToken);
            }
            else
            {
                var entity = collection.First();
                entity.FieldTitle = dto.FieldTitle;
                entity.Type = dto.Type;
                await repository.UpdateAsync(entity, cancellationToken);
            }
        }
        return new(true, "Tempalte created");
    }
}