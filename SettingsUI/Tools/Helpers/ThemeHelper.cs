﻿using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;
using Windows.Storage;

namespace SettingsUI.Helpers;

public static class ThemeHelper
{
    private const string SelectedAppThemeKey = "SelectedAppTheme";
    private static Window _CurrentWindow;

    /// <summary>
    /// Gets the current actual theme of the app based on the requested theme of the
    /// root element, or if that value is Default, the requested theme of the Application.
    /// </summary>
    public static ElementTheme ActualTheme
    {
        get
        {
            if (_CurrentWindow.Content is FrameworkElement rootElement)
            {
                if (rootElement.RequestedTheme != ElementTheme.Default)
                {
                    return rootElement.RequestedTheme;
                }
            }

            return GeneralHelper.GetEnum<ElementTheme>(Application.Current.RequestedTheme.ToString());
        }
    }

    /// <summary>
    /// Gets or sets (with LocalSettings persistence) the RequestedTheme of the root element.
    /// </summary>
    public static ElementTheme RootTheme
    {
        get
        {
            if (_CurrentWindow.Content is FrameworkElement rootElement)
            {
                return rootElement.RequestedTheme;
            }

            return ElementTheme.Default;
        }
        set
        {
            if (_CurrentWindow.Content is FrameworkElement rootElement)
            {
                rootElement.RequestedTheme = value;
            }

            ApplicationData.Current.LocalSettings.Values[SelectedAppThemeKey] = value.ToString();
            UpdateSystemCaptionButtonColors();
        }
    }

    public static void Initialize(Window CurrentWindow)
    {
        // Save reference as this might be null when the user is in another app
        _CurrentWindow = CurrentWindow;

        string savedTheme = ApplicationData.Current.LocalSettings.Values[SelectedAppThemeKey]?.ToString();

        if (savedTheme != null)
        {
            RootTheme = GeneralHelper.GetEnum<ElementTheme>(savedTheme);
        }
    }

    public static bool IsDarkTheme()
    {
        if (RootTheme == ElementTheme.Default)
        {
            return Application.Current.RequestedTheme == ApplicationTheme.Dark;
        }
        return RootTheme == ElementTheme.Dark;
    }

    public static void UpdateSystemCaptionButtonColors()
    {
        AppWindow m_AppWindow = WindowHelper.GetAppWindowForCurrentWindow(_CurrentWindow);
        var titleBar = m_AppWindow.TitleBar;
        titleBar.ButtonBackgroundColor = Colors.Transparent;
        titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        if (IsDarkTheme())
        {
            titleBar.ButtonForegroundColor = Colors.White;
            titleBar.ButtonInactiveForegroundColor = Colors.White;
        }
        else
        {
            titleBar.ButtonForegroundColor = Colors.Black;
            titleBar.ButtonInactiveForegroundColor = Colors.Black;
        }
    }

    /// <summary>
    /// Use This Method in RadioButtonChecked event
    /// </summary>
    /// <param name="sender"></param>
    public static void OnThemeRadioButtonChecked(object sender)
    {
        var selectedTheme = ((RadioButton)sender)?.Tag?.ToString();
        if (selectedTheme != null)
        {
            RootTheme = GeneralHelper.GetEnum<ElementTheme>(selectedTheme);
        }
    }

    public static void SetThemeRadioButtonChecked(Panel ThemePanel)
    {
        var currentTheme = RootTheme.ToString();
        (ThemePanel.Children.Cast<RadioButton>().FirstOrDefault(c => c?.Tag?.ToString() == currentTheme)).IsChecked = true;
    }
}
