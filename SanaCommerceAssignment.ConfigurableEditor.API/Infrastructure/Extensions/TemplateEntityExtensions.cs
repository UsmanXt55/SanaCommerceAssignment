using SanaCommerceAssignment.ConfigurableEditor.API.Models.Entities;
using SanaCommerceAssignment.ConfigurableEditor.Shared.DTOs.Templates;
namespace SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Extensions;
public static class TemplateEntityExtensions
{
    public static GetDto ToDto(this TemplateEntity entity)
    {
        return new(entity.Id, entity.PageId, entity.FieldId,entity.FieldTitle, entity.Type, entity.Type.GetInputType(), entity.Type.GetInputReadonly());
    }
}
