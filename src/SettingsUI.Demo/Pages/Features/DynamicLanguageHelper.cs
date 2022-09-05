using System.IO;
using SettingsUI.Tools;

namespace SettingsUI.Demo.Pages;
public static class DynamicLanguageHelper
{
    //for UnPackaged App
    public static string resourcesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Strings");
    public static Localizer Localizer { get; set; } = Localizer.GetCurrent(resourcesFolderPath);
}
