using SanaCommerceAssignment.AddonSystem.AddonBase;
namespace AddonSample.OutputBasedAddonSample01;
public class OutputBasedAddon : IOutputBaseAddon
{
    public OutputBaseAddonOption GetMessage() => new("Hello World", OutputEnum.Console);
}
