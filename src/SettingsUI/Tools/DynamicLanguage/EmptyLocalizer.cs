// LICENSE https://github.com/AndrewKeepCoding/AK.Toolkit

namespace SettingsUI.Tools;
public class EmptyLocalizer : ILocalizer
{
    public static readonly ILocalizer Instance = new EmptyLocalizer();

    public IEnumerable<string> GetAvailableLanguages() => Enumerable.Empty<string>();

    public string GetCurrentLanguage() => string.Empty;

    public StringResourceListDictionary? GetLanguageResources(string language) => null;

    public string? GetLocalizedString(string key, string? language = null) => null;

    public void InitializeWindow(UIElement Content)
    {
    }

    public void RegisterRootElement(FrameworkElement rootElement)
    {
    }

    public void RunLocalization(FrameworkElement rootElement, string? language = null)
    {
    }

    public void RunLocalizationOnRegisteredRootElements(string? language = null)
    {
    }

    public bool TryRegisterUIElementChildrenGetters(Type type, Func<UIElement, IEnumerable<UIElement>> func) => false;

    public bool TrySetCurrentLanguage(string language) => false;
}
