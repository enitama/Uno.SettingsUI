using System.Collections.Generic;

namespace SettingsUI.Helpers;

public static class ResourceHelper
{
    /// <summary>
    /// Get All Resources Keys
    /// </summary>
    /// <param name="identifier">Default value is Resources</param>
    /// <returns></returns>
    public static List<string> GetAllResourcesKeys(string identifier = null)
    {
        List<string> reslist = new List<string>();

        Windows.ApplicationModel.Resources.Core.ResourceMap rmap = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap;
        foreach (string str in rmap.Keys)
        {
            if (str.StartsWith(identifier ?? "Resources"))
            {
                reslist.Add($"/{str}");
            }
        }

        return reslist;
    }

    public static string GetString(string key, string language = "en-US")
    {
        Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = language;
        var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse();
        return resourceLoader.GetString(key);
    }

    public static string GetString(string key)
    {
        var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForViewIndependentUse();
        return resourceLoader.GetString(key);
    }
}
