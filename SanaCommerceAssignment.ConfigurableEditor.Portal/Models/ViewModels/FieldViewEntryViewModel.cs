namespace SanaCommerceAssignment.ConfigurableEditor.Portal.Models.ViewModels;
public record FieldViewEntryViewModel(string Name, string Type, bool ReadOnly, string Value) : FieldViewModel(Name, Type);