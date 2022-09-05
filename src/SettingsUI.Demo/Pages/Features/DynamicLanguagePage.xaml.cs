using Microsoft.UI.Xaml.Controls;

namespace SettingsUI.Demo.Pages;
public sealed partial class DynamicLanguagePage : Page
{
   
    public DynamicLanguagePage()
    {
        this.InitializeComponent();
        Loaded += DynamicLanguagePage_Loaded;
    }

    private void DynamicLanguagePage_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        DynamicLanguageHelper.Localizer.RunLocalizationOnRegisteredRootElements();
    }

    private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        DynamicLanguageHelper.Localizer.TrySetCurrentLanguage("en-US");
    }

    private void Button_Click_1(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        DynamicLanguageHelper.Localizer.TrySetCurrentLanguage("fa-IR");
    }
}
