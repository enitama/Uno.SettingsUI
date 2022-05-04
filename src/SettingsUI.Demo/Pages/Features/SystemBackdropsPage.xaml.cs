using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SettingsUI.Helpers;

namespace SettingsUI.Demo.Pages;

public sealed partial class SystemBackdropsPage : Page
{
    public SystemBackdropsHelper backdropsHelper = SystemBackdropsHelper.GetCurrent();

    public SystemBackdropsPage()
    {
        this.InitializeComponent();
    }

    private void ChangeBackdropButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        BackdropType newType;
        switch (backdropsHelper.CurrentBackdrop)
        {
            case BackdropType.Mica: newType = BackdropType.DesktopAcrylic; break;
            case BackdropType.DesktopAcrylic: newType = BackdropType.DefaultColor; break;
            default:
            case BackdropType.DefaultColor: newType = BackdropType.Mica; break;
        }
        backdropsHelper.SetBackdrop(newType);
    }

    private void btnAcrylic_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var window = WindowHelper.CreateWindow();
        window.Content = new Grid();
        SystemBackdropsHelper backdropsHelper = SystemBackdropsHelper.CreateInstance();
        backdropsHelper.Initialize(window, BackdropType.DesktopAcrylic);

        window.Activate();

    }

    private void btnMica_Click(object sender, RoutedEventArgs e)
    {
        var window = WindowHelper.CreateWindow();
        window.Content = new Grid();
        SystemBackdropsHelper backdropsHelper = SystemBackdropsHelper.CreateInstance();
        backdropsHelper.Initialize(window, BackdropType.Mica);

        window.Activate();
    }
}
