using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace SettingsUI.Helpers;

public static class NativeMethods
{
    [DllImport("User32.dll")]
    internal static extern int GetDpiForWindow(IntPtr hwnd);

    [DllImport("User32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

    [DllImport("Shcore.dll", SetLastError = true)]
    internal static extern int GetDpiForMonitor(IntPtr hmonitor, Monitor_DPI_Type dpiType, out uint dpiX, out uint dpiY);

    [DllImport("kernel32.dll", SetLastError = false, ExactSpelling = true, CharSet = CharSet.Unicode)]
    internal static extern int GetCurrentPackageFullName(ref uint packageFullNameLength, [Optional] StringBuilder packageFullName);

    internal enum Monitor_DPI_Type : int
    {
        MDT_Effective_DPI = 0,
        MDT_Angular_DPI = 1,
        MDT_Raw_DPI = 2,
        MDT_Default = MDT_Effective_DPI
    }

    /// <summary>
    /// Places the window at the top of the Z order.
    /// </summary>
    internal static readonly IntPtr HWND_TOP = new IntPtr(0);

    /// <summary>
    /// An attribute applied to native pointer parameters to indicate its semantics
    /// such that a friendly overload of the method can be generated with the right signature.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    [Conditional("CodeGeneration")]
    public class FriendlyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FriendlyAttribute"/> class.
        /// </summary>
        /// <param name="flags">The flags that describe this parameter's semantics.</param>
        public FriendlyAttribute(FriendlyFlags flags)
        {
            this.Flags = flags;
        }

        /// <summary>
        /// Gets the flags that describe this parameter's semantics.
        /// </summary>
        public FriendlyFlags Flags { get; }

        /// <summary>
        /// Gets or sets the 0-based index to the parameter that takes the length of the array given by this array parameter.
        /// An overload will be generated that drops this parameter and sets it automatically based on the length of the array provided to the parameter this attribute is applied to.
        /// </summary>
        /// <value>-1 is the default and indicates that no automated parameter removal should be generated.</value>
        /// <remarks>
        /// Only applicable when <see cref="Flags"/> includes <see cref="FriendlyFlags.Array"/>.
        /// </remarks>
        public int ArrayLengthParameter { get; set; } = -1;
    }

    [Flags]
    public enum FriendlyFlags
    {
        /// <summary>
        /// The pointer is to the first element in an array.
        /// </summary>
        Array = 0x1,

        /// <summary>
        /// The value is at least partially initialized by the caller.
        /// </summary>
        In = 0x2
    }

    [Flags]
    public enum SetWindowPosFlags : uint
    {
        /// <summary>
        ///     Retains the current position (ignores X and Y parameters).
        /// </summary>
        SWP_NOMOVE = 0x0002
    }
}
