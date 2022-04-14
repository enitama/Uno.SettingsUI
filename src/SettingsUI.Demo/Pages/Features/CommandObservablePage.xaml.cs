using Microsoft.UI.Xaml.Controls;
using SettingsUI.Demo.Pages.Features;

namespace SettingsUI.Demo.Pages
{
    public sealed partial class CommandObservablePage : Page
    {
        public CommandObservableViewModel Vm { get; set; }

        public CommandObservablePage()
        {
            this.InitializeComponent();
            Vm = new CommandObservableViewModel();
            DataContext = Vm;
        }
    }
}
