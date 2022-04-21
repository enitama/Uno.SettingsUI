using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace SettingsUI.Helpers;
public class MaterialHelper
{
    private static IntPtr _hWnd;
    public static void MakeTransparent(object target)
    {
        _hWnd = WindowHelper.GetWindowHandleForCurrentWindow(target);
        NativeMethods.SubClassDelegate = new(NativeMethods.WindowSubClass);
        NativeMethods.SetWindowSubclass(_hWnd, NativeMethods.SubClassDelegate, 0, 0);

        long nExStyle = NativeMethods.GetWindowLong(_hWnd, NativeMethods.GWL_EXSTYLE);
        if ((nExStyle & NativeMethods.WS_EX_LAYERED) == 0)
        {
            NativeMethods.SetWindowLong(_hWnd, NativeMethods.GWL_EXSTYLE, (IntPtr) (nExStyle | NativeMethods.WS_EX_LAYERED));
            NativeMethods.SetLayeredWindowAttributes(_hWnd, (uint) ColorTranslator.ToWin32(Color.Magenta), 255, NativeMethods.LWA_COLORKEY);
        }
    }

    public static void SetMica(bool Enable, bool DarkMode)
    {
        int IsMicaEnabled = Enable ? 1 : 0;
        NativeMethods.DwmSetWindowAttribute(_hWnd, (int) NativeMethods.DWMWINDOWATTRIBUTE.DWMWA_MICA_EFFECT, ref IsMicaEnabled, sizeof(int));

        int IsDarkEnabled = DarkMode ? 1 : 0;
        NativeMethods.DwmSetWindowAttribute(_hWnd, (int) NativeMethods.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, ref IsDarkEnabled, sizeof(int));
    }

    public static void SetAcrylic(bool Enable, bool DarkMode) =>
        SetComposition(NativeMethods.AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND, Enable, DarkMode);

    public static void SetBlur(bool Enable, bool DarkMode) =>
        SetComposition(NativeMethods.AccentState.ACCENT_ENABLE_BLURBEHIND, Enable, DarkMode);

    public static void SetComposition(NativeMethods.AccentState AccentState, bool Enable, bool DarkMode)
    {
        var Accent = Enable ? new NativeMethods.AccentPolicy()
        {
            AccentState = AccentState,
            GradientColor = Convert.ToUInt32(DarkMode ? 0x990000 : 0xFFFFFF)
        } : new NativeMethods.AccentPolicy() { AccentState = 0 };

        var StructSize = Marshal.SizeOf(Accent);
        var Ptr = Marshal.AllocHGlobal(StructSize);
        Marshal.StructureToPtr(Accent, Ptr, false);

        var data = new NativeMethods.WindowCompositionAttributeData()
        {
            Attribute = NativeMethods.WindowCompositionAttribute.WCA_ACCENT_POLICY,
            SizeOfData = StructSize,
            Data = Ptr
        };

        NativeMethods.SetWindowCompositionAttribute(_hWnd, ref data);
        Marshal.FreeHGlobal(Ptr);
    }
}
