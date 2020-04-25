using System;
using System.Runtime.InteropServices;

namespace Titan.Windows.Win32
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct WNDCLASSEXA
    {
        [MarshalAs(UnmanagedType.U4)]
        public int CbSize;
        [MarshalAs(UnmanagedType.U4)]
        public int Style;
        public IntPtr LpFnWndProc;
        public int CbClsExtra;
        public int CbWndExtra;
        public IntPtr HInstance;
        public IntPtr HIcon;
        public IntPtr HCursor;
        public IntPtr HbrBackground;
        public string LpszMenuName;
        public string LpszClassName;
        public IntPtr HIconSm;
    }
}
