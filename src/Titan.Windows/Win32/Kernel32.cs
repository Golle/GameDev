using System;
using System.Runtime.InteropServices;

namespace Titan.Windows.Win32
{
    internal class User32
    {
        private const string User32Dll = "user32";

        internal delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, UIntPtr wParam, UIntPtr lParam);

        [DllImport(User32Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern ushort RegisterClassExA(
            [In] ref WNDCLASSEXA wndClassEx
            );

        [DllImport(User32Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int ShowWindow(
            IntPtr hWnd, 
            ShowWindow nCmdShow
            );

        [DllImport(User32Dll, SetLastError = true)]
        public static extern IntPtr CreateWindowExA(
            WindowStylesEx dwExStyle,
            string lpClassName,
            string lpWindowName,
            WindowStyles dwStyle,
            int x,
            int y,
            int nWidth,
            int nHeight,
            IntPtr hWndParent,
            IntPtr hMenu,
            IntPtr hInstance,
            IntPtr lpParam
            );

        [DllImport(User32Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr DefWindowProcA(
            IntPtr hWnd, 
            uint msg,
            UIntPtr wParam,
            UIntPtr lParam
            );

        [DllImport(User32Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekMessageA(
            ref MSG lpMsg, 
            IntPtr hWnd,
            uint wMsgFilterMin, 
            uint wMsgFilterMax, 
            uint removeMessage
            );

        [DllImport(User32Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool TranslateMessage(
            [In] ref MSG lpMsg
            );

        [DllImport(User32Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr DispatchMessage(
            [In] ref MSG lpMsg
            );

    }
}
