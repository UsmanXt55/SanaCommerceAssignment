using System.Reflection;
namespace SanaCommerceAssignment.AddonSystem.ConsoleApp.Helpers;
internal static class AddonHelpers
{
    internal static T[] LoadAddonAssemblies<T>(string folderPath, MethodInfo implementedMethodInfo)
    {
        var addonFiles = Directory.GetFiles(folderPath, "*.dll");
        var addons = addonFiles
            .SelectMany(file => Assembly.LoadFrom(file).GetTypes())
            .Where(t => typeof(T).IsAssignableFrom(t) && !t.IsInterface && MethodImplemented(t, implementedMethodInfo))
            .Select(t => (T)Activator.CreateInstance(t))
            .ToArray();

        return addons;
    }

    private static bool MethodImplemented(Type type, MethodInfo method)
    {
        return type.GetMethod(method.Name)?.DeclaringType == type;
    }
}
