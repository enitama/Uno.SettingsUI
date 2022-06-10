using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SettingsUI.Demo.AppNotification;
using SettingsUI.Helpers;

namespace SettingsUI.Demo.Pages;
public sealed partial class AppNotificationPage : Page
{
    internal static AppNotificationPage Instance;
    public AppNotificationPage()
    {
        this.InitializeComponent();
        Instance = this;
    }

    public void NotificationReceived(Notification notification)
    {
        if (notification.HasInput)
        {
            txtReceived.Text = notification.Input;
        }
        else
        {
            txtReceived.Text = "Notification Received";
        }
    }
    public void NotificationInvoked(string message)
    {
        txtInvoked.Text = message;
    }
    private void btnToast1_Click(object sender, RoutedEventArgs e)
    {
        if (!ApplicationHelper.IsPackaged)
        {
            ToastWithAvatar.Instance.SendToast();
        }
    }

    private void btnToast2_Click(object sender, RoutedEventArgs e)
    {
        if (!ApplicationHelper.IsPackaged)
        {
            ToastWithTextBox.Instance.SendToast();
        }
    }

    private void btnToast3_Click(object sender, RoutedEventArgs e)
    {
        if (!ApplicationHelper.IsPackaged)
        {
            ToastWithPayload.Instance.SendToast();
        }
    }
}
