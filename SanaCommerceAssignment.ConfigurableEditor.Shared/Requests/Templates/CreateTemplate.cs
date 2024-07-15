using SanaCommerceAssignment.ConfigurableEditor.Shared.Enums;
namespace SanaCommerceAssignment.ConfigurableEditor.Shared.Requests.Templates;
public record CreateTemplateRequest(string PageId, IEnumerable<CreateTemplateRequestField> Fields);
public record CreateTemplateRequestField(string FieldId, string FieldTitle, FieldTypeEnum Type);