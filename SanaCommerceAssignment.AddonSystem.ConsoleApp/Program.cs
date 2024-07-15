using SanaCommerceAssignment.AddonSystem.AddonBase;
using SanaCommerceAssignment.AddonSystem.ConsoleApp.Helpers;
internal class Program
{
    private static void Main(string[] args)
    {
        var addonsFolder = "./addons";

        var textBasedAddons = AddonHelpers.LoadAddonAssemblies<ITextBaseAddon>(addonsFolder, typeof(ITextBaseAddon).GetMethod("GetMessage")!);
        var outputBasedAddons = AddonHelpers.LoadAddonAssemblies<IOutputBaseAddon>(addonsFolder, typeof(IOutputBaseAddon).GetMethod("GetMessage")!);

        var textBasedAddonMessages = string.Join(Environment.NewLine, textBasedAddons.Select(mp => GetMessage(mp)));
        var consoleOutputBasedAddonMessages = string.Join(Environment.NewLine, outputBasedAddons.Where(x => x.GetMessage().outputStream == OutputEnum.Console).Select(mp => mp.GetMessage().Message));
        var fileOutputBasedAddonMessages = string.Join(Environment.NewLine, outputBasedAddons.Where(x => x.GetMessage().outputStream == OutputEnum.TextFile).Select(mp => mp.GetMessage().Message));

        CreateOutputTextFile(fileOutputBasedAddonMessages);
        Console.WriteLine(textBasedAddonMessages);
        Console.WriteLine(consoleOutputBasedAddonMessages);
    }

    private static string GetMessage(ITextBaseAddon addon)
    {
        try
        {
            return addon.GetMessage();
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
    private static void CreateOutputTextFile(string fileOutputBasedAddonMessages)
    {
        if (string.IsNullOrWhiteSpace(fileOutputBasedAddonMessages)) return;
        var outputDirectory = "output";
        var filePath = $"{outputDirectory}/{DateTime.Now.ToString("yyyyMMdd")}.txt";
        if(!Directory.Exists(outputDirectory))
            Directory.CreateDirectory(outputDirectory);
        
        File.AppendAllText(filePath, fileOutputBasedAddonMessages);
    }
}