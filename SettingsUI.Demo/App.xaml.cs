using Microsoft.UI.Xaml;
using SettingsUI.Helpers;

namespace SettingsUI.Demo
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            ThemeHelper.Initialize(m_window);
            m_window.Activate();
        }

        private Window m_window;
    }
}
