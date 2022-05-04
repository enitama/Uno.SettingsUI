using Microsoft.UI.Xaml;
using WinRT; // required to support Window.As<ICompositionSupportsSystemBackdrop>()

namespace SettingsUI.Helpers;
public enum BackdropType
{
    Mica,
    DesktopAcrylic,
    DefaultColor,
}
public class SystemBackdropsHelper
{
    private Window _window;
    private WindowsSystemDispatcherQueueHelper m_wsdqHelper;
    public BackdropType CurrentBackdrop;
    public Microsoft.UI.Composition.SystemBackdrops.MicaController m_micaController;
    public Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController m_acrylicController;
    public Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration m_configurationSource;
    internal static SystemBackdropsHelper Instance;

    public static SystemBackdropsHelper CreateInstance()
    {
        return new SystemBackdropsHelper();
    }

    /// <summary>
    /// Returns the previously created instance. a new object will be create if it is not created.
    /// </summary>
    /// <returns></returns>
    public static SystemBackdropsHelper GetCurrent()
    {
        if (Instance == null)
        {
            Instance = new SystemBackdropsHelper();
        }

        return Instance;
    }

    public void Initialize(Window CurrentWindow)
    {
        _window = CurrentWindow;

        m_wsdqHelper = new WindowsSystemDispatcherQueueHelper();
        m_wsdqHelper.EnsureWindowsSystemDispatcherQueueController();
    }

    public void Initialize(Window CurrentWindow, BackdropType CurrentType)
    {
        Initialize(CurrentWindow);
        SetBackdrop(CurrentType);
    }

    /// <summary>
    /// Reset to default color. If the requested type is supported, we'll update to that.
    /// Note: This sample completely removes any previous controller to reset to the default
    ///       state. This is done so this sample can show what is expected to be the most
    ///       common pattern of an app simply choosing one controller type which it sets at
    ///       startup. If an app wants to toggle between Mica and Acrylic it could simply
    ///       call RemoveSystemBackdropTarget() on the old controller and then setup the new
    ///       controller, reusing any existing m_configurationSource and Activated/Closed
    ///       event handlers. 
    /// </summary>
    /// <param name="type"></param>
    public void SetBackdrop(BackdropType type)
    {
        ThemeHelper.SystemBackdropsType = type;
        CurrentBackdrop = BackdropType.DefaultColor;
        if (m_micaController != null)
        {
            m_micaController.Dispose();
            m_micaController = null;
        }
        if (m_acrylicController != null)
        {
            m_acrylicController.Dispose();
            m_acrylicController = null;
        }
        _window.Activated -= Window_Activated;
        _window.Closed -= Window_Closed;
        m_configurationSource = null;

        if (type == BackdropType.Mica)
        {
            if (TrySetMicaBackdrop())
            {
                CurrentBackdrop = type;
            }
            else
            {
                // Mica isn't supported. Try Acrylic.
                type = BackdropType.DesktopAcrylic;
            }
        }
        if (type == BackdropType.DesktopAcrylic)
        {
            if (TrySetAcrylicBackdrop())
            {
                CurrentBackdrop = type;
            }
            else
            {
                // Acrylic isn't supported, so take the next option, which is DefaultColor, which is already set.
            }
        }
    }

    public bool TrySetMicaBackdrop()
    {
        if (Microsoft.UI.Composition.SystemBackdrops.MicaController.IsSupported())
        {
            // Hooking up the policy object
            m_configurationSource = new Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration();
            _window.Activated += Window_Activated;
            _window.Closed += Window_Closed;

            // Initial configuration state.
            m_configurationSource.IsInputActive = true;

            switch (((FrameworkElement) _window.Content).ActualTheme)
            {
                case ElementTheme.Dark: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Dark; break;
                case ElementTheme.Light: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Light; break;
                case ElementTheme.Default: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Default; break;
            }

            m_micaController = new Microsoft.UI.Composition.SystemBackdrops.MicaController();

            // Enable the system backdrop.
            // Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
            m_micaController.AddSystemBackdropTarget(_window.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
            m_micaController.SetSystemBackdropConfiguration(m_configurationSource);
            return true; // succeeded
        }

        return false; // Mica is not supported on this system
    }

    public bool TrySetAcrylicBackdrop()
    {
        if (Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController.IsSupported())
        {
            // Hooking up the policy object
            m_configurationSource = new Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration();
            _window.Activated += Window_Activated;
            _window.Closed += Window_Closed;

            // Initial configuration state.
            m_configurationSource.IsInputActive = true;

            switch (((FrameworkElement) _window.Content).ActualTheme)
            {
                case ElementTheme.Dark: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Dark; break;
                case ElementTheme.Light: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Light; break;
                case ElementTheme.Default: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Default; break;
            }

            m_acrylicController = new Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController();

            // Enable the system backdrop.
            // Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
            m_acrylicController.AddSystemBackdropTarget(_window.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
            m_acrylicController.SetSystemBackdropConfiguration(m_configurationSource);
            return true; // succeeded
        }

        return false; // Acrylic is not supported on this system
    }

    private void Window_Activated(object sender, WindowActivatedEventArgs args)
    {
        m_configurationSource.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
    }

    private void Window_Closed(object sender, WindowEventArgs args)
    {
        // Make sure any Mica/Acrylic controller is disposed so it doesn't try to
        // use this closed window.
        if (m_micaController != null)
        {
            m_micaController.Dispose();
            m_micaController = null;
        }
        if (m_acrylicController != null)
        {
            m_acrylicController.Dispose();
            m_acrylicController = null;
        }
        _window.Activated -= Window_Activated;
        m_configurationSource = null;
    }
}
