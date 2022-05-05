namespace SettingsUI.Helpers;
public static class ApplicationHelper
{
    private const uint APPMODEL_ERROR_NO_PACKAGE = 15700;
    public static bool IsPackaged { get; } = GetCurrentPackageName() != null;

    public static string GetCurrentPackageName()
    {
        var length = 0u;
        NativeMethods.GetCurrentPackageFullName(ref length);

        var result = new StringBuilder((int)length);
        var error = NativeMethods.GetCurrentPackageFullName(ref length, result);

        if (error == APPMODEL_ERROR_NO_PACKAGE)
            return null;

        return result.ToString();
    }
}

