using Microsoft.UI.Xaml.Controls;
using SettingsUI.Helpers;

namespace SettingsUI.Demo.Pages;

public sealed partial class MaterialPage : Page
{
    public MaterialPage()
    {
        this.InitializeComponent();
    }

    private void btnMica_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var micaWindow = WindowHelper.CreateWindow();
        micaWindow.Activate();
        MaterialHelper.MakeTransparent(micaWindow);
        MaterialHelper.SetMica(true, chkIsDark.IsChecked.Value);
    }

    private void btnAcrylic_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var acrylicWindow = WindowHelper.CreateWindow();
        acrylicWindow.Activate();
        MaterialHelper.MakeTransparent(acrylicWindow);
        MaterialHelper.SetAcrylic(true, chkIsDark.IsChecked.Value);
    }

    private void btnBlur_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var blurWindow = WindowHelper.CreateWindow();
        blurWindow.Activate();
        MaterialHelper.MakeTransparent(blurWindow);
        MaterialHelper.SetBlur(true, chkIsDark.IsChecked.Value);
    }
}
