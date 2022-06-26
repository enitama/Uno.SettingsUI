using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SettingsUI.Helpers;

namespace SettingsUI.Demo.Pages;

public sealed partial class SystemBackdropsPage : Page
{
    public SystemBackdropsPage()
    {
        this.InitializeComponent();
    }

    private void ChangeBackdropButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        BackdropType newType;
        switch (ThemeHelper.GetSystemBackdropType())
        {
            case BackdropType.Mica: newType = BackdropType.DesktopAcrylic; break;
            case BackdropType.DesktopAcrylic: newType = BackdropType.DefaultColor; break;
            default:
            case BackdropType.DefaultColor: newType = BackdropType.Mica; break;
        }
        ThemeHelper.ChangeSystemBackdropType(newType);
    }

    private void btnAcrylic_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var window = WindowHelper.CreateWindow();
        window.Content = new Grid { RequestedTheme = ThemeHelper.RootTheme };
        SystemBackdropsHelper backdropsHelper = new SystemBackdropsHelper(window);
        backdropsHelper.ChangeSystemBackdropType(BackdropType.DesktopAcrylic);

        window.Activate();

    }

    private void btnMica_Click(object sender, RoutedEventArgs e)
    {
        var window = WindowHelper.CreateWindow();
        window.Content = new Grid { RequestedTheme = ThemeHelper.RootTheme };
        SystemBackdropsHelper backdropsHelper = new SystemBackdropsHelper(window);
        backdropsHelper.ChangeSystemBackdropType(BackdropType.Mica);

        window.Activate();
    }
}
