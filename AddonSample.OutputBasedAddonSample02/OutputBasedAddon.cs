using SanaCommerceAssignment.AddonSystem.AddonBase;
namespace AddonSample.OutputBasedAddonSample02;
public class OutputBasedAddon : IOutputBaseAddon
{
    public OutputBaseAddonOption GetMessage() => new("Hello", OutputEnum.TextFile);    
}
