// LICENSE https://github.com/AndrewKeepCoding/AK.Toolkit

namespace SettingsUI.Tools;

public interface ILocalizer
{
    void InitializeWindow(FrameworkElement Root, UIElement Content);

    IEnumerable<string> GetAvailableLanguages();

    string GetCurrentLanguage();

    string? GetLocalizedString(string key, string? language = null);

    StringResourceListDictionary? GetLanguageResources(string language);

    bool TrySetCurrentLanguage(string language);

    void RegisterRootElement(FrameworkElement rootElement);

    void RunLocalizationOnRegisteredRootElements(string? language = null);

    void RunLocalization(FrameworkElement rootElement, string? language = null);

    bool TryRegisterUIElementChildrenGetters(Type type, Func<UIElement, IEnumerable<UIElement>> func);
}
