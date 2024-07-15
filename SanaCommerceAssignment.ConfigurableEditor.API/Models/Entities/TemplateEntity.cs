using SanaCommerceAssignment.ConfigurableEditor.Shared.Enums;
namespace SanaCommerceAssignment.ConfigurableEditor.API.Models.Entities;
public class TemplateEntity
{
    public int Id { get; set; }
    public string PageId { get; set; }
    public string FieldId { get; set; }
    public string FieldTitle { get; set; }
    public FieldTypeEnum Type { get; set; }
}
