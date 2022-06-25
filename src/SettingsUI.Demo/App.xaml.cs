using System.Collections.Generic;
using System;
using Microsoft.UI.Xaml;
using Microsoft.Windows.AppNotifications;
using SettingsUI.Demo.AppNotification;
using SettingsUI.Helpers;
using SettingsUI.Demo.Pages;

namespace SettingsUI.Demo;

public partial class App : Application
{
    private NotificationManager notificationManager;
    public App()
    {
        this.InitializeComponent();
        if (!ApplicationHelper.IsPackaged)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            var c_notificationHandlers = new Dictionary<int, Action<AppNotificationActivatedEventArgs>>();
            c_notificationHandlers.Add(ToastWithAvatar.Instance.ScenarioId, ToastWithAvatar.Instance.NotificationReceived);
            c_notificationHandlers.Add(ToastWithTextBox.Instance.ScenarioId, ToastWithTextBox.Instance.NotificationReceived);
            c_notificationHandlers.Add(ToastWithPayload.Instance.ScenarioId, ToastWithPayload.Instance.NotificationReceived);
            notificationManager = new NotificationManager(c_notificationHandlers);
        }
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        m_window = new MainWindow();
        ThemeHelper.Initialize(m_window, BackdropType.Mica);
        if (!ApplicationHelper.IsPackaged)
        {
            notificationManager.Init(notificationManager, OnNotificationInvoked);
        }
        m_window.Activate();
    }
    private void OnNotificationInvoked(string message)
    {
        AppNotificationPage.Instance.NotificationInvoked(message);
    }

    void OnProcessExit(object sender, EventArgs e)
    {
        notificationManager.Unregister();
    }
    private Window m_window;
}
