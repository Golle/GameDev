using System;
using System.Runtime.InteropServices;

namespace Titan.Windows.Win32
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MSG
    {
        internal IntPtr Hwnd;
        internal WindowsMessage Message;
        internal IntPtr WParam;
        internal IntPtr LParam;
        internal ulong Time;
        internal POINT Point;
        internal ulong LPrivate;
    }
}
