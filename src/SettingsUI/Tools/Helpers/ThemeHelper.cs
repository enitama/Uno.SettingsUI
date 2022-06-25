namespace SettingsUI.Helpers;

public static class ThemeHelper
{
    private const string SelectedAppThemeKey = "SelectedAppTheme";
    private static Window CurrentApplicationWindow;

    /// <summary>
    /// Gets the current actual theme of the app based on the requested theme of the
    /// root element, or if that value is Default, the requested theme of the Application.
    /// </summary>
    public static ElementTheme ActualTheme
    {
        get
        {
            foreach (Window window in WindowHelper.ActiveWindows)
            {
                if (window.Content is FrameworkElement rootElement)
                {
                    if (rootElement.RequestedTheme != ElementTheme.Default)
                    {
                        return rootElement.RequestedTheme;
                    }
                }
            }

            if (CurrentApplicationWindow != null && CurrentApplicationWindow.Content is FrameworkElement element)
            {
                if (element.RequestedTheme != ElementTheme.Default)
                {
                    return element.RequestedTheme;
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
            foreach (Window window in WindowHelper.ActiveWindows)
            {
                if (window.Content is FrameworkElement rootElement)
                {
                    return rootElement.RequestedTheme;
                }
            }
            if (CurrentApplicationWindow != null && CurrentApplicationWindow.Content is FrameworkElement element)
            {
                return element.RequestedTheme;
            }
            return ElementTheme.Default;
        }
        set
        {
            foreach (Window window in WindowHelper.ActiveWindows)
            {
                if (window.Content is FrameworkElement rootElement)
                {
                    rootElement.RequestedTheme = value;
                }
            }

            if (CurrentApplicationWindow != null && CurrentApplicationWindow.Content is FrameworkElement element)
            {
                element.RequestedTheme = value;
            }

            if (ApplicationHelper.IsPackaged)
            {
                ApplicationData.Current.LocalSettings.Values[SelectedAppThemeKey] = value.ToString();
            }
            else
            {
                Internal.UnPackagedSetting.SaveTheme(value.ToString());
            }
            UpdateSystemCaptionButtonColors();
        }
    }

    /// <summary>
    /// If you are using WindowHelper.CreateWindow, you can set window = null
    /// </summary>
    /// <param name="window"></param>
    public static void Initialize(Window window)
    {
        CurrentApplicationWindow = window;
        Initialize();
    }

    /// <summary>
    /// If you are using WindowHelper.CreateWindow, you can set window = null
    /// </summary>
    /// <param name="window"></param>
    /// <param name="backdropType"></param>
    public static void Initialize(Window window, BackdropType backdropType)
    {
        CurrentApplicationWindow = window;
        Initialize();

        foreach (Window _window in WindowHelper.ActiveWindows)
        {
            var _backdropsHelper = new SystemBackdropsHelper(_window);
            _backdropsHelper.ChangeBackdrop(backdropType);
        }

        if (CurrentApplicationWindow != null)
        {
            var backdropsHelper = new SystemBackdropsHelper(CurrentApplicationWindow);
            backdropsHelper.ChangeBackdrop(backdropType);
        }
    }

    private static void Initialize()
    {
        string savedTheme = string.Empty;
        
        if (ApplicationHelper.IsPackaged)
        {
            savedTheme = ApplicationData.Current.LocalSettings.Values[SelectedAppThemeKey]?.ToString();
        }
        else
        {
            savedTheme = Internal.UnPackagedSetting.ReadTheme();
        }
        if (savedTheme != null)
        {
            RootTheme = GeneralHelper.GetEnum<ElementTheme>(savedTheme);
        }
        UpdateSystemCaptionButtonColors();
    }

    public static bool IsDarkTheme()
    {
        if (RootTheme == ElementTheme.Default)
        {
            return Application.Current.RequestedTheme == ApplicationTheme.Dark;
        }
        return RootTheme == ElementTheme.Dark;
    }

    private static void UpdateSystemCaptionButton(AppWindow appWindow)
    {
        var titleBar = appWindow.TitleBar;
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
    public static void UpdateSystemCaptionButtonColors()
    {
        foreach (Window window in WindowHelper.ActiveWindows)
        {
            var appWindow = WindowHelper.GetAppWindowForCurrentWindow(window);
            UpdateSystemCaptionButton(appWindow);
        }

        if (CurrentApplicationWindow != null)
        {
            var appWindow = WindowHelper.GetAppWindowForCurrentWindow(CurrentApplicationWindow);
            UpdateSystemCaptionButton(appWindow);
        }
    }

    /// <summary>
    /// Use This Method in RadioButtonChecked event
    /// </summary>
    /// <param name="sender"></param>
    public static void OnThemeRadioButtonChecked(object sender)
    {
        var selectedTheme = ((RadioButton) sender)?.Tag?.ToString();
        if (selectedTheme != null)
        {
            RootTheme = GeneralHelper.GetEnum<ElementTheme>(selectedTheme);
        }
        UpdateSystemCaptionButtonColors();
    }

    public static void SetThemeRadioButtonChecked(Panel ThemePanel)
    {
        var currentTheme = RootTheme.ToString();
        (ThemePanel.Children.Cast<RadioButton>().FirstOrDefault(c => c?.Tag?.ToString() == currentTheme)).IsChecked = true;
    }
}
