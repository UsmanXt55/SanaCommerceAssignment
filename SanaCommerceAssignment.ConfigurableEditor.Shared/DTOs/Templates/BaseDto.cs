using SanaCommerceAssignment.ConfigurableEditor.Shared.Enums;
namespace SanaCommerceAssignment.ConfigurableEditor.Shared.DTOs.Templates;
public record BaseDto(int Id, string PageId, string FieldId, string FieldTitle, FieldTypeEnum Type);