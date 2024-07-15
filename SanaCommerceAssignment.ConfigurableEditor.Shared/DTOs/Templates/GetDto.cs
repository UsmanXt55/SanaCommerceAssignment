using SanaCommerceAssignment.ConfigurableEditor.Shared.Enums;
namespace SanaCommerceAssignment.ConfigurableEditor.Shared.DTOs.Templates;
public record GetDto(int Id, string PageId, string FieldId, string FieldTitle, FieldTypeEnum Type, string FieldType, bool Readonly) : BaseDto(Id, PageId, FieldId, FieldTitle, Type);