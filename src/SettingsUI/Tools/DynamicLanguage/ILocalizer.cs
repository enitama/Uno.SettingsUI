// LICENSE https://github.com/AndrewKeepCoding/AK.Toolkit

namespace SettingsUI.Tools;

public interface ILocalizer
{
    StringResourceListDictionary? GetLanguageResources(string language);

    IEnumerable<string> GetAvailableLanguages();

    string GetCurrentLanguage();

    bool TrySetCurrentLanguage(string language);

    void RegisterRootElement(FrameworkElement rootElement);

    void RunLocalizationOnRegisteredRootElements(string? language = null);

    void RunLocalization(FrameworkElement rootElement, string? language = null);

    string? GetLocalizedString(string key, string? language = null);

    bool TryRegisterUIElementChildrenGetters(Type type, Func<UIElement, IEnumerable<UIElement>> func);
}
