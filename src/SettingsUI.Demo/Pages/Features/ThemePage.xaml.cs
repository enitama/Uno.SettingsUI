using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SettingsUI.Helpers;

namespace SettingsUI.Demo.Pages;

public sealed partial class ThemePage : Page
{
    public ThemePage()
    {
        this.InitializeComponent();
    }

    private void OnThemeRadioButtonChecked(object sender, RoutedEventArgs e)
    {
        ThemeHelper.OnThemeRadioButtonChecked(sender);
    }

    private void SettingsPageControl_Loaded(object sender, RoutedEventArgs e)
    {
        ThemeHelper.SetThemeRadioButtonChecked(ThemePanel);
    }
}
