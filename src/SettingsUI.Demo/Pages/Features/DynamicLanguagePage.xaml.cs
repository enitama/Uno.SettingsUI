using System.Linq;
using Microsoft.UI.Xaml.Controls;
using SettingsUI.Tools;

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
        Localizer.Get().RunLocalization(Root);
    }

    private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        Localizer.Get().SetLanguage("en-US");
    }

    private void Button_Click_1(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        Localizer.Get().SetLanguage("fa-IR");
    }

    private void Button_Click_2(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        txt.Text = Localizer.Get().GetLocalizedStrings("langDetail").FirstOrDefault();
    }
}
