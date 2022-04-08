using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SettingsUI.Helpers;

namespace SettingsUI.Demo.Pages
{
    public sealed partial class ApplicationDataContainerPage : Page
    {
        ApplicationDataContainerHelper settings = ApplicationDataContainerHelper.GetCurrent();
        public ApplicationDataContainerPage()
        {
            this.InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            settings.Save<string>("stringKey", txtString.Text);
            settings.Save<double>("doubleKey", txtNumber.Value);
            settings.Save<bool>("boolKey", chkBool.IsChecked.Value);
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            txtString2.Text = settings.Read<string>("stringKey");
            txtNumber2.Value = settings.Read<double>("doubleKey");
            chkBool2.IsChecked = settings.Read<bool>("boolKey");
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            settings.Clear();
            btnLoad_Click(null, null);
        }
    }
}
