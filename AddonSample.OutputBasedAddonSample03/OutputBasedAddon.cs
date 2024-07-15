using SanaCommerceAssignment.AddonSystem.AddonBase;
namespace AddonSample.OutputBasedAddonSample03;
public class OutputBasedAddon : IOutputBaseAddon
{
    public OutputBaseAddonOption GetMessage() => new("World", OutputEnum.TextFile);
}
