using Riok.Mapperly.Abstractions;
using SanaCommerceAssignment.ConfigurableEditor.API.Models.Entities;
using SanaCommerceAssignment.ConfigurableEditor.Shared.DTOs.Templates;
namespace SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Mappers;
[Mapper]
public partial class TemplateMapper
{
    public partial BaseDto MapToDto(TemplateEntity entity);
    public partial TemplateEntity MapToEntity(BaseDto dto);
    public partial List<TemplateEntity> MapToEntity(List<BaseDto> dto);
    public partial List<BaseDto> MapToDto(List<TemplateEntity> entity);
}