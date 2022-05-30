namespace SettingsUI.Internal;
internal static class UnPackagedSetting
{
    public static readonly string AppName = "SettingsUI2.0";
    public static readonly string RootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName);
    public static readonly string AppConfigPath = Path.Combine(RootPath, "AppConfig.txt");

    public static void SaveTheme(string value)
    {
        File.WriteAllText(AppConfigPath, value);
    }
    
    public static string ReadTheme()
    {
        return File.ReadAllText(AppConfigPath);
    }
}
