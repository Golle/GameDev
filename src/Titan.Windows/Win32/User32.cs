using System;
using System.Runtime.InteropServices;

namespace Titan.Windows.Win32
{
    internal class User32
    {
        internal delegate IntPtr WndProcDelegate(IntPtr hWnd, WindowsMessage msg, UIntPtr wParam, UIntPtr lParam);

        private const string User32Dll = "user32";

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
            [In] IntPtr hWnd,
            [In] WindowsMessage msg,
            [In] UIntPtr wParam,
            [In] UIntPtr lParam
        );

        [DllImport(User32Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekMessageA(
            [Out] out MSG lpMsg,
            [In] IntPtr hWnd,
            [In] uint wMsgFilterMin,
            [In] uint wMsgFilterMax,
            [In] uint removeMessage
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

        [DllImport(User32Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern void PostQuitMessage(
            [In] int nExitCode
        );

        [DllImport(User32Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyWindow(
            [In] IntPtr hWnd
        );

        [DllImport(User32Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowTextA(
            [In] IntPtr hWnd,
            [In] string lpString
        );

        [DllImport(User32Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(
            [Out] out POINT lpPoint
        );

        [DllImport(User32Dll, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ScreenToClient(
            [In] IntPtr hWnd,
            ref POINT lpPoint
        );
    }
}
