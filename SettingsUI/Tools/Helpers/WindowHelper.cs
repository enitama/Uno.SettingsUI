using Microsoft.UI;
using Microsoft.UI.Windowing;
using System;

namespace SettingsUI.Helpers;

public static class WindowHelper
{
    public static AppWindow GetAppWindowForCurrentWindow(object target)
    {
        IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(target);
        WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
        return AppWindow.GetFromWindowId(wndId);
    }

    public static void SetWindowSize(IntPtr hwnd, int width, int height)
    {
        // Win32 uses pixels and WinUI 3 uses effective pixels, so you should apply the DPI scale factor
        var dpi = NativeMethods.GetDpiForWindow(hwnd);
        float scalingFactor = (float)dpi / 96;
        width = (int)(width * scalingFactor);
        height = (int)(height * scalingFactor);

        NativeMethods.SetWindowPos(hwnd, NativeMethods.HWND_TOP, 0, 0, width, height, NativeMethods.SetWindowPosFlags.SWP_NOMOVE);
    }
}
